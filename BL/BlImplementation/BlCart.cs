using BlApi;
using BO;
using Dal;
using DalApi;

namespace BlImplementation;

internal class BlCart : ICart
{
    private IDal Dal = new DalList();
    /// <summary>
    /// Add a product to cart or add amount of the product
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="productId"></param>
    /// <returns></returns>
    /// <exception cref="BO.StockNotValidExcpetion"></exception>
    public BO.Cart Add(BO.Cart cart, int productId)
    {

        BO.OrderItem orderItem= new BO.OrderItem();    
        BO.Product product = new BO.Product();
        DO.Products products = new DO.Products();
        products = Dal.Product.Get(productId, 0);

        if(!cart.orderItems.Exists(OrderItem=> OrderItem.ProductID==productId))// test if the product not exist in the Cart
        {
            if (Dal.Product.GetList().ToList().Exists(Product=> product.ID==productId))
            {
                 if (products.InStock == 0)
                      throw new BO.StockNotValidExcpetion("Invalid stock!");// rupture de stock

                 else
                 {
                     product.ID = products.Id;
                     product.Name = products.Name;
                     product.Price = products.Price;
                     product.Category = (BO.Category)products.Category;
                     product.InStock = products.InStock;
                     orderItem.ProductID=productId;
                     orderItem.Name=product.Name;
                     orderItem.Price=product.Price;
                     orderItem.Amount = 1;
                     orderItem.TotalPrice = orderItem.Amount * orderItem.Price;
                     cart.TotalPrice += orderItem.TotalPrice;
                     cart.orderItems.Add(orderItem);
                 }
            }
            else
            {
                throw new Exception("product not exist");
            }
        }
        else
        {
            if (products.InStock== 0)
                throw new BO.StockNotValidExcpetion("Invalid stock!");// rupture de stock
            else
            {
                int index=cart.orderItems.FindIndex(x => x.ID==products.Id);
                cart.orderItems[index].Amount += 1;
                cart.orderItems[index].TotalPrice += products.Price;
                cart.TotalPrice += cart.orderItems[index].TotalPrice;
            }
        }
        return cart;
    }

    public Cart Confirmation(Cart cart, string Name, string Email, string Address)
    {
        throw new NotImplementedException();
    }

    public Cart Update(Cart cart, int productId, int newAmount)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Update the amount of Product in the Cart
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="productId"></param>
    /// <param name="newAmount"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    //public BO.Cart Update(BO.Cart cart, int productId, int newAmount)
    //{
    //    if (newAmount > cart.Items.Amount)
    //    {
    //        cart = Add(cart, productId);
    //    }

    //    else if (newAmount < cart.Items.Amount)
    //    {
    //        cart.Items.Amount = newAmount;
    //        BO.Product product = new BO.Product();
    //        product.ID = productId;
    //        DO.Products products = new DO.Products();
    //        products = Dal.Product.Get(productId, 0);
    //        product.Name = products.Name;
    //        product.Price = products.Price;
    //        product.Category = (BO.Category)products.Category;
    //        product.InStock = products.InStock + (cart.Items.Amount - newAmount);
    //        cart.Items.TotalPrice -= (cart.Items.Price * (cart.Items.Amount - newAmount));
    //        cart.TotalPrice -= cart.Items.TotalPrice;
    //        return cart;
    //    }

    //    //else if (newAmount == 0)
    //    //{
    //    //    //cart.Items = Delete() fonction bonus de BlOrder
    //    //}
    //    throw new Exception();
    //}


}
