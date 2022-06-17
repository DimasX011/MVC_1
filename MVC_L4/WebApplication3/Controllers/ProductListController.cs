using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class ProductListController : Controller
    {
        private TreadSafeCollection _collection = new();

        public IActionResult ProductList()
        {
            return View(_collection.GetAll());
        }

    }
}
