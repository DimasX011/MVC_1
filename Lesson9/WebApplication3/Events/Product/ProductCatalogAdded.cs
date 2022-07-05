using WebApplication3.Models;


namespace WebApplication3.Events.Product;

public class ProductCatalogAdded : IDomainEvent
{
    public ProductModel Product { get; }
    
    public TypeMessage Message { get; }

    public ProductCatalogAdded(ProductModel productModel, TypeMessage message)
    {
        Product = productModel;
        Message = message;
    }
}
