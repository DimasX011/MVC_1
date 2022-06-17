using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using WebApplication3.Services;
using WebApplication3.Interfaces;
using Microsoft.Extensions.Options;

namespace WebApplication3.Controllers
{
    public class ProductController : Controller
    {

        private TreadSafeCollection _collection = new TreadSafeCollection();
        private EmailService _emailService = new EmailService();
        private ProductCatalogService _IproductService = new ProductCatalogService();

        public ProductController(IOptions<EmailService> emailService, IOptions<ProductCatalogService> productCatalogService, IOptions<TreadSafeCollection> options)
        {
            _emailService = emailService.Value;
            _IproductService = productCatalogService.Value;
            _collection = options.Value;
        }

        [HttpGet]
        public IActionResult Products()
        {
          return View(_collection.GetAll());
        }

        [HttpPost]
        public IActionResult Products([FromForm]ProductModel exemp)
        {
             _collection.Add(exemp);
             _IproductService.ToConverter();
             _emailService.SendEmail("Dimon1998daf@mail.ru", "В магазин был добавлен новый товар", "Добавлен товар с id " + exemp.Id + ", наименованием " + exemp.Name + ", описанием " + exemp.Description + ", типом " + exemp.Type,  "Дмитрия ", "Яковлева");
             return View(_collection.GetAll());
        }
       
    }
}
