using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers;

public class MetricsController : Controller
{
    [HttpGet]
    public IActionResult Metrics()
    {
        return View(RequestLoggingMiddleware.GetData());
    }
}