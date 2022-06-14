using System.Collections.Concurrent;
using WebApplication3;
    
namespace WebApplication3.Models
{
    public class ProductsCatalog
    {
        private static TreadSafeCollection GetProductBag { get; set; } = new ();
        private static List<ProductModel> products { get; set; } = new();

        public static List<ProductModel> ToConverter()
        {
            List<ProductModel>Somemodel  = new();
            Somemodel = GetProductBag.ToListConvert();
            return Somemodel;
        }

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
