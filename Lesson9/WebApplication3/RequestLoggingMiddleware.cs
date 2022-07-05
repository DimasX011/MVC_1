using System.Net.NetworkInformation;
using WebApplication3.Models;

namespace WebApplication3;
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;
    private static int HomeCount;
    private static int PrivacyCount;
    private static int ProductCount;
    private static int DeleteCount;
    private static int ListCountView;
    private static int CatalogView;

    public RequestLoggingMiddleware(RequestDelegate next,                                                                                                              
        ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation("Request Method: {Method}", context.Request.Method);
        _logger.LogInformation("number of page transitions {Method}", context.Request.Path + context.Request.Path.Value);   
        if(context.Request.Path.Value == "/")
        {
            HomeCount++;
        }
        else if (context.Request.Path.Value == "/Home/Privacy")
        {
            PrivacyCount++;
        }
        else if(context.Request.Path.Value == "/Product/Products")
        {
            ProductCount++;
        }
        else if(context.Request.Path.Value == "/DeleteProducts/DeleteProducts")
        {
            DeleteCount++;
        }
        else if(context.Request.Path.Value == "/ProductList/ProductList")
        {
            ListCountView++;
        }
        else if(context.Request.Path.Value == "/Catalog/Categories")
        {
            CatalogView++;
        }
        await _next(context);
    }

    public static SiteStatistic GetData()
    {
        SiteStatistic _statistic = new();
        _statistic._HomeCount = HomeCount;
        _statistic._CatalogView = CatalogView;
        _statistic._DeleteCount = DeleteCount;
        _statistic._PrivacyCount = PrivacyCount;
        _statistic._ProductCount = ProductCount;
        _statistic._ListCountView = ListCountView;
        return _statistic;
    }
}