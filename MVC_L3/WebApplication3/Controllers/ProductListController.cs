using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class ProductListController : Controller
    {
        public static TreadSafeCollection collection = new();

        public IActionResult ProductList()
        {
            return View(collection.GetAll());
        }

    }
}
