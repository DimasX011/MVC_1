using Momentum.Exceptions;
using WebApplication3.Events.Product;
using WebApplication3.Interfaces;
using Polly;
using WebApplication3.Services;

namespace WebApplication3.Events.EventConsumers;

public class ProductAddedEmailHandler : BackgroundService
{
    private readonly EmailService _emailService;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<ProductAddedEmailHandler> _logger;
    private CancellationToken _cancellationToken;
    
    public ProductAddedEmailHandler(ILogger<ProductAddedEmailHandler> logger, IServiceScopeFactory serviceScopeFactory, EmailService service)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        _emailService = service;
        DomainEventsManager.Register<ProductCatalogAdded>(e => {SendEmailNotification(e); });
    }
    private async Task SendEmailNotification(ProductCatalogAdded productCatalogAdded)
    {
        await using var scope = _serviceScopeFactory.CreateAsyncScope();
        var emailSender = scope.ServiceProvider.GetRequiredService<EmailService>();
        Task SendAsync(CancellationToken cancellationToken)
        {
            if (productCatalogAdded.Message._isAdded == true)
            {
                return emailSender.SendEmail("Dimon1998daf@mail.ru", "В магазин был добавлен новый товар",
                    "Добавлен товар с id " + productCatalogAdded.Product.Id + ", наименованием " +
                    productCatalogAdded.Product.Name + ", описанием " + productCatalogAdded.Product.Description +
                    ", типом " + productCatalogAdded.Product.Type, "Дмитрия ", "Яковлева", cancellationToken);
            }
            else if(productCatalogAdded.Message._isRemoved == true)
            {
                return emailSender.SendEmail("Dimon1998daf@mail.ru", "C магазина был убран товар", "Убран товар с id " 
                    + productCatalogAdded.Product.Id + ", наименованием " + productCatalogAdded.Product.Name + ", описанием "
                    + productCatalogAdded.Product.Description + ", типом " + productCatalogAdded.Product.Type, "Дмитрия ", "Яковлева", cancellationToken);
            }
            else if(productCatalogAdded.Message._isSecurity == true)
            {
                return  emailSender.SendEmail("Dimon1998daf@mail.ru", "Проверка работоспособности сервиса", 
                    "Сервис работает" + DateTime.Now,"Службы ", "Контроля", cancellationToken);
            }
            return Task.CompletedTask;
        }
        var policy = Policy.Handle<ConnectionException>().WaitAndRetryAsync(3,
            retryAttemp => TimeSpan.FromSeconds(Math.Pow(retryAttemp, 1)),
            (exception, retryAttemp) =>
            {
                _logger.LogWarning(exception,
                    "There was an error while sending email. Retrying: {attempt}", retryAttemp);
            });
        var result = await policy.ExecuteAndCaptureAsync(SendAsync, _cancellationToken);
        if (result.Outcome == OutcomeType.Failure)
        {
            _logger.LogError(result.FinalException,"There was an error while sending email");
        }
    }
    
    protected override Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _cancellationToken = cancellationToken;
        return  Task.CompletedTask;
    }


}