using WebApplication3.Services;

namespace WebApplication3;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;
    private static TreadSafeCollection _collection = new TreadSafeCollection();
    
    public RequestLoggingMiddleware(RequestDelegate next,
        ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string path = context.Request.Path.Value;
        _logger.LogInformation("Request Method: {Method}", context.Request.Method);
        _logger.LogInformation("number of page transitions {Method}",
            context.Request.Path + context.Request.Path.Value);
        _collection.GetUpdate(path);
        await _next(context);
    }

}