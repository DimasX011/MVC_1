﻿using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using WebApplication3.Services;
using Microsoft.Extensions.Options;
using WebApplication3.Events.EventConsumers;
using WebApplication3.Events.Product;

namespace WebApplication3.Controllers
{
    public class ProductController : Controller
    {
        
        private TreadSafeCollection _collection = new TreadSafeCollection();
        private ProductCatalogService _IproductService = new ProductCatalogService();
        private CancellationTokenSource cts = new CancellationTokenSource();

        public ProductController( IOptions<ProductCatalogService> productCatalogService, IOptions<TreadSafeCollection> options)
        {
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
            var ct = cts.Token;
            if (!(ct.IsCancellationRequested))
            {
                _collection.Add(exemp,ct);
                _IproductService.ToConverter();
            }
            return View(_collection.GetAll());
        }
       
    }
}
