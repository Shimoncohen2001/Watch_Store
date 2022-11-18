using BlApi;
using BO;

namespace BlImplementation;

sealed public class Bl : IBl
{
    public IProduct Product => new BlProduct();

    public IOrder Order => new BlOrder();

    public IOrderItem OrderItem => new BlOrderItem();

    public ICart Cart => new BlCart();

    public IProductItem ProductItem => new BlProductItem();

    public IProductForList ProductForList => new BlProductForList();

    public IOrderTracking OrderTracking => new BlOrderTracking();

    public IOrderForList OrderForList => new BlOrderForList();
}
