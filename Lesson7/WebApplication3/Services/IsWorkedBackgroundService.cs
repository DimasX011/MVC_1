using System.Diagnostics;

namespace WebApplication3.Services;

public class IsWorkedBackgroundService : BackgroundService
{
    private EmailService _emailService = new ();

    public IsWorkedBackgroundService(EmailService emailService)
    {
        _emailService = emailService;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromMinutes(60));
        Stopwatch sw = Stopwatch.StartNew();
        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            _emailService.SendEmail("Dimon1998daf@mail.ru", "Проверка работоспособности сервиса", "Сервис работает" + DateTime.Now,"Службы ", "Контроля");
        }
    }

}