using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using WebApplication3.Services;
using WebApplication3.Interfaces;
using Microsoft.Extensions.Options;

namespace WebApplication3.Controllers
{
    public class DeleteProductsController : Controller
    {
        private TreadSafeCollection _collection = new TreadSafeCollection();
        private ProductCatalogService _IproductService = new ProductCatalogService();
        private EmailService _emailService = new EmailService();

        public DeleteProductsController(IOptions<EmailService> emailService, IOptions<ProductCatalogService> productCatalogService, IOptions<TreadSafeCollection> options)
        {
            _emailService = emailService.Value;
            _IproductService = productCatalogService.Value;
            _collection = options.Value;
        }

        [HttpGet]
        public IActionResult DeleteProducts()
        {
            return View(_collection.GetAll());
        }

        [HttpPost]
        public IActionResult DeleteProducts([FromForm] ProductModel exemp)
        {
            _collection.Remove(exemp);
            _IproductService.ToConverter();
            _emailService.SendEmail("Dimon1998daf@mail.ru", "C магазина был убран товар", "Убран товар с id " + exemp.Id + ", наименованием " + exemp.Name + ", описанием " + exemp.Description + ", типом " + exemp.Type, "Дмитрия ", "Яковлева");
            return View(_collection.GetAll());
        }
    }
}
