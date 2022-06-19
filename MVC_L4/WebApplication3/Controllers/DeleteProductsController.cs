using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using WebApplication3.Services;
using WebApplication3.Interfaces;

namespace WebApplication3.Controllers
{
    public class DeleteProductsController : Controller
    {
        public static TreadSafeCollection _collection = new();
        private IProductCatalogService _IproductService = new ProductCatalogService();
        private IEmailService _emailService = new EmailService();

        public DeleteProductsController(IEmailService emailService, IProductCatalogService productCatalogService)
        {
            _emailService = emailService;
            _IproductService = productCatalogService;
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
