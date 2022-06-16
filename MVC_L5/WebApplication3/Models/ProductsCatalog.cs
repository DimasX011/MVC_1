using System.Collections.Concurrent;
using WebApplication3;
    
namespace WebApplication3.Models
{
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
