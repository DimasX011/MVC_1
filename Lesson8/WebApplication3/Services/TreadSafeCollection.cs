using System.Collections.Concurrent;
using WebApplication3.Events;
using WebApplication3.Events.Product;
using WebApplication3.Interfaces;

namespace WebApplication3.Services;

public class TreadSafeCollection : ITreadSafeCollection
{
    private static object _Synobject = new();

    private static readonly ConcurrentDictionary<int, Models.ProductModel> _dictionary = new();
    public int Count => _dictionary.Count;
    public void Add(Models.ProductModel productModel, CancellationToken token)
    {
        if(!(token.IsCancellationRequested))
        {
            _dictionary.TryAdd(productModel.Id, productModel);
            DomainEventsManager.Raise(new ProductCatalogAdded(productModel, new TypeMessage(){_isAdded = true, _isRemoved = false, _isSecurity = false}));
        }
    }

    public void Remove(Models.ProductModel product)
    {
        _dictionary.TryRemove(product.Id, out product);
        DomainEventsManager.Raise(new ProductCatalogAdded(product, new TypeMessage(){_isRemoved = true, _isAdded = false, _isSecurity = false}));
    }
       
    public IReadOnlyCollection<Models.ProductModel> GetAll() => _dictionary.Values.ToArray();

    public List<Models.ProductModel> ToListConvert()
    {
        List<Models.ProductModel> products = new();
        if (_dictionary.Count == 0)
        {
            lock (_Synobject)
            {
                products.Clear();
            }
        }
        else
        {
            lock (_Synobject)
            {
                products.Clear();
                foreach (var c in _dictionary.Values)
                {
                    products.Add(c);
                }
            }
        }
        return products;
    }
}