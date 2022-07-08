using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApplication3.Services;

namespace WebApplication3.Controllers;

public class MetricsController : Controller
{
    private TreadSafeCollection _collection = new();
    public MetricsController(IOptions<TreadSafeCollection> options)
    {
        _collection = options.Value;
    }
    
    [HttpGet]
    public IActionResult Metrics()
    {
        return View(_collection);
    }
}