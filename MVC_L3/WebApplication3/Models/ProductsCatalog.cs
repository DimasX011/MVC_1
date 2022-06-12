using System.Collections.Concurrent;
    
namespace WebApplication3.Models
{
    public class ProductsCatalog
    {
        public List<ProductModel> GetProducts { get; set; } = new();
        
        public ConcurrentList<ProductModel> GeGetProductsConc { get; set; } = new();
        public ConcurrentBag<ProductModel> GetProductBag { get; set; } = new ();
        
        public static IEnumerable<ProductModel> GenerateProduct(int i)
        {
            for(int c = 0; c< i; c ++)
            {
                 yield return new ProductModel { Id = c, Name = "", Description = "", Type = "" };
            }
        }
    }

    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        public FormUrlEncodedContent ToFormData()
        {
            return new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>(nameof(Name), Name),
            new KeyValuePair<string, string>(nameof(Id), Id.ToString()),
            new KeyValuePair<string, string>(nameof(Description), Description),
            new KeyValuePair<string, string>(nameof(Type), Type)
        });
        }
    }

}
