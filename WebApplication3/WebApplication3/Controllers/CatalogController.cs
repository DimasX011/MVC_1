using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;


namespace WebApplication3.Controllers
{
    public class CatalogController  : Controller
    {
        private static Catalog _catalog = new();
        private static ProductsCatalog __catalog = new();


        [HttpGet]
        public IActionResult Categories()
        {
            return View(_catalog);
        }

        [HttpPost]
        public IActionResult Categories(Category model)
        {
            _catalog.GetCategories.Add(model);
            return View(_catalog);
        }
    }
}
