using BO;

namespace BlApi;

/// <summary>
/// ////////////////////IBoProduct Interface////////////////////////
/// </summary>
public interface IProduct
{
    public void Add(Product product);
    public IEnumerable<ProductForList> GetProductList();
}
