using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApplication3.Interfaces;
using WebApplication3.Models;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{
    public class ProductListController : Controller
    {
        private TreadSafeCollection _collection = new TreadSafeCollection();

        public ProductListController(IOptions<TreadSafeCollection> options)
        {
            _collection = options.Value;
        }

        public IActionResult ProductList()
        {
            return View(_collection.GetAll());
        }

    }
}
