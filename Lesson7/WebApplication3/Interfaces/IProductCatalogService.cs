using WebApplication3.Models;

namespace WebApplication3.Interfaces
{
    public interface IProductCatalogService
    {
        public void ToConverter();

        public IEnumerable<ProductModel> GenerateProduct(int i);

    }
}
