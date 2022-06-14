using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using static WebApplication3.Models.ProductsCatalog;

namespace WebApplication3.Controllers
{
    public class ProductController : Controller
    {
       
      
        public static TreadSafeCollection collection = new();
        

        [HttpGet]
        public IActionResult Products()
        {
          return View(collection.GetAll());
        }

        [HttpPost]
        public IActionResult Products([FromForm]ProductModel exemp)
        {
             collection.Add(exemp);
             return View(collection.GetAll());
        }
       
    }
}
