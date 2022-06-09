using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class ProductListController : Controller
    {
        public IActionResult ProductList()
        {
            return View(ProductController._catalog);
        }

    }
}
