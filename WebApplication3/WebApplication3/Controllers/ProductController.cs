using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using static WebApplication3.Models.ProductsCatalog;

namespace WebApplication3.Controllers
{
    public class ProductController : Controller
    {
        public static ProductsCatalog _catalog = new();
        private static object _SyncObj = new();
        

        [HttpGet]
        public IActionResult Products()
        {
          return View(_catalog);
        }

        [HttpPost]
        public IActionResult Products([FromForm]ProductModel exemp)
        {
             _catalog.GeGetProductsConc.Add(exemp);
             return View(_catalog);
        }
    }
}
