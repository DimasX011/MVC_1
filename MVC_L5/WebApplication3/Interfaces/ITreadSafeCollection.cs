namespace WebApplication3.Interfaces
{
    public interface ITreadSafeCollection
    {
        public void Add(Models.ProductModel productModel);

        public void Remove(Models.ProductModel product);

        public IReadOnlyCollection<Models.ProductModel> GetAll();

        public List<Models.ProductModel> ToListConvert();
    }
}
