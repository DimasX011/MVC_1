using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class DeleteProductsController : Controller
    {
        public static TreadSafeCollection collection = new();

        [HttpGet]
        public IActionResult DeleteProducts()
        {
            return View(collection.GetAll());
        }

        [HttpPost]
        public IActionResult DeleteProducts([FromForm] ProductModel exemp)
        {
            collection.Remove(exemp);
            collection.ToListConvert();
            return View(collection.GetAll());
        }
    }
}
