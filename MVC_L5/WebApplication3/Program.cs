using Serilog;
using Serilog.Events;
using WebApplication3.Controllers;
using WebApplication3.Interfaces;
using WebApplication3.Models;
using WebApplication3.Services;
using Polly.Retry;
using Polly;
using Polly.Extensions.Http;

Log.Logger = new LoggerConfiguration()
 .WriteTo.Console()
 .CreateBootstrapLogger();
Log.Information("Starting up");
try
{
    var builder = WebApplication.CreateBuilder(args);
   
    // Add services to the container.
    builder.Services.AddControllersWithViews();
    builder.Host.UseSerilog((_, conf) => conf.WriteTo.Console());
    builder.Host.UseSerilog((_, conf) =>
    {
        conf
        .WriteTo.Console()
        .WriteTo.File("log-.txt", (Serilog.Events.LogEventLevel)RollingInterval.Day)
        ;
    });
    builder.Host.UseSerilog((ctx, conf) =>
    {
        conf
        .MinimumLevel.Debug()
     .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
     .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
     .ReadFrom.Configuration(ctx.Configuration)
     ;
    });
    builder.Services.Configure<EmailService>(builder.Configuration.GetSection("EmailService"));
    builder.Services.Configure<ProductCatalogService>(builder.Configuration.GetSection("ProductCatalogService"));
    builder.Services.AddScoped<IEmailService, EmailService>();
    builder.Services.AddScoped<IProductCatalogService, ProductCatalogService>();
    builder.Services.AddHttpClient<IEmailService, EmailService>().SetHandlerLifetime(TimeSpan.FromMinutes(5)).AddPolicyHandler(GetRetryPolicy());

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseSerilogRequestLogging();
   // app.MapGet("/", () => "Hello World!");

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


