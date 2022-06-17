using WebApplication3.Interfaces;
using WebApplication3.Models;

namespace WebApplication3.Services
{
    public class ProductCatalogService : IProductCatalogService
    {
        private static TreadSafeCollection GetProductBag { get; set; } = new();
        public static List<ProductModel> products { get; set; } = new();

        public void ToConverter()
        {
            List<ProductModel> Somemodel = new();
            Somemodel = GetProductBag.ToListConvert();
            products = Somemodel;
        }

        public IEnumerable<ProductModel> GenerateProduct(int i)
        {
            for (int c = 0; c < i; c++)
            {
                yield return new ProductModel { Id = c, Name = "", Description = "", Type = "" };
            }
        }
    }
}
