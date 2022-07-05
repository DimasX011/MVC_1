using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using WebApplication3.Interfaces;
using WebApplication3.Services;
using Polly;
using Polly.Extensions.Http;
using WebApplication3;
using WebApplication3.Controllers;
using WebApplication3.Events.EventConsumers;
 
Log.Logger = new LoggerConfiguration()
 .WriteTo.Console()
 .CreateBootstrapLogger();
Log.Information("Starting up");
try
{
    var builder = WebApplication.CreateBuilder(args);
    
    builder.Services.AddControllersWithViews();
    builder.Host.UseSerilog((ctx, conf) =>
    {
        conf
        .MinimumLevel.Debug()
     .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
     .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
     .ReadFrom.Configuration(ctx.Configuration)
     ;
    });
    
    builder.Services.AddSingleton<EmailService>();
    builder.Services.AddHostedService<IsWorkedBackgroundService>();
    builder.Services.AddSingleton<IProductCatalogService, ProductCatalogService>();
    builder.Services.AddHostedService<ProductAddedEmailHandler>();
    builder.Services.Configure<SmtpConfig>(builder.Configuration.GetSection("SmtpConfig"));
    builder.Services.AddSingleton<IEmailService, EmailService>();

    
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
  
    app.Use(async (HttpContext context, Func<Task> next) =>
    {
        var userAgent = context.Request.Headers.UserAgent.ToString();
        
        if (!userAgent.Contains("Edg"))
        {
            context.Response.ContentType = "text/plain; charset=UTF-8";
           await context.Response.WriteAsync("Ваш браузер не поддерживается");
           return;
        }
        await next();
    });
    app.UseMiddleware<RequestLoggingMiddleware>();
    app.UseStaticFiles();
    app.UseSerilogRequestLogging();
    

   app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    app.Run();

    static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions.HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,
                                                                        retryAttempt)));
    }
    
    
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush(); 
}

#pragma warning disable CS8618
public class SmtpConfig
{
    public string Host { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public int Port { get; set; }
    
    public bool EnableLogging { get; set; }

  
}


