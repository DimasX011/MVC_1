using System.Collections.Concurrent;

namespace WebApplication3;

public class TreadSafeCollection
{
    public static List<Models.ProductModel> products = new ();

    private static readonly ConcurrentDictionary<int, Models.ProductModel> _dictionary = new();
    public int Count => _dictionary.Count;
    public void Add(Models.ProductModel productModel) => _dictionary.TryAdd(productModel.Id, productModel);
    public void Remove(Models.ProductModel product) => _dictionary.TryRemove(product.Id, out product);
    public  IReadOnlyCollection<Models.ProductModel> GetAll() => _dictionary.Values.ToArray();

    public void ToListConvert()
    {
        if(_dictionary.Count == 0)
        {
            products.Clear();
        }

        else
        {
            products.Clear();
            foreach (var c in _dictionary.Values)
            {
                products.Add(c);
            }
        }
    }
}