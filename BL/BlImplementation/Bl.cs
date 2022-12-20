using BlApi;
using BO;

namespace BlImplementation;

sealed internal class Bl : IBl
{
    public IProduct Product => new BlProduct();
    public IOrder Order => new BlOrder();
    public ICart Cart => new BlCart();
}