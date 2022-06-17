namespace WebApplication3.Models
{
    public class Catalog
    {
        public List<Category> GetCategories { get; set; } = new();
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
