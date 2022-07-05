using System.Diagnostics;
using WebApplication3.Events;
using WebApplication3.Events.Product;
using WebApplication3.Interfaces;
using WebApplication3.Models;

namespace WebApplication3.Services;

public class IsWorkedBackgroundService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromMinutes(60));
        Stopwatch sw = Stopwatch.StartNew();
        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            DomainEventsManager.Raise(new ProductCatalogAdded(new ProductModel(), new TypeMessage(){_isRemoved = false, _isAdded = false, _isSecurity = true}));
        }
    }

}