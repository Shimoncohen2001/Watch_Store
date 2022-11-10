using DO;

namespace Dal;
public class DalOrderItems
{
    /// <summary>
    /// ////////////////////////////Add//////////////////////////
    /// </summary>
    /// <param name="orderItems"></param>
    public void AddOrderItems(OrderItems orderItems)
    {
        DataSource.GetAddOrderItemToList(orderItems);
    }

    /// <summary>
    /// ///////////////////////////GetById//////////////////////
    /// </summary>
    /// <param name="OrderId"></param>
    /// <param name="ProductId"></param>
    /// <returns></returns>
    public OrderItems GetOrderItems(int OrderId, int ProductId)
    {
         OrderItems orderItems = new OrderItems();
         foreach (var item in DataSource._orderItems)
         {
             if(item.OrderId == OrderId && item.ProductId == ProductId)
                 orderItems = item;
         }
         return orderItems;
    }

    /// <summary>
    /// ///////////////////////GetList/////////////////////////
    /// </summary>
    /// <returns></returns>
    public OrderItems[] GetOrderItemsList()
    {
        return DataSource._orderItems;
    }

    /// <summary>
    /// //////////////////////Delete///////////////////////////
    /// </summary>
    /// <param name="ProductId"></param>
    /// <param name="orderId"></param>
    public void DeletOrderItem(int ProductId,int orderId)
    {
        int count = 0;
        foreach (var item in GetOrderItemsList())
        {
            if (item.OrderId == orderId && item.ProductId == ProductId)
            {
                for (int i = count; i < DataSource.Config._orderItemsIndex; i++)// Shifting elements
                {
                    GetOrderItemsList()[i] = GetOrderItemsList()[i + 1];
                }
            }
            count++;
        }
    }

    /// <summary>
    /// //////////////////////////Update///////////////////////
    /// </summary>
    /// <param name="orderId"></param>
    public void UpdateOrderItem(int orderId, int productId)
    {
        int count = 0;
        foreach (var item in GetOrderItemsList())
        {
            if (item.OrderId == orderId && item.ProductId == productId )
            {
                OrderItems NewOrderItem = new OrderItems();
                NewOrderItem = GetOrderItems(orderId, productId);
                bool finish = false;
                while (!finish)
                {
                     Console.WriteLine(@"choose option:
                                           1. Update the Amount of your product:
                                           2. EXIT ");
                     int choice = 0;
                     int.TryParse(Console.ReadLine(), out choice);
                     if (choice == 1) 
                     {
                         DalProducts dalProducts = new DalProducts();
                         Console.WriteLine("Enter the new Amount: ");
                         int NewAmount = 0;
                         int.TryParse(Console.ReadLine(), out NewAmount);
                         NewOrderItem.Amount = NewAmount;
                         NewOrderItem.Price = NewAmount * dalProducts.GetProduct(productId).Price;
                     }
                     else // exit 
                     {
                         Console.WriteLine("Amount updated.");
                         GetOrderItemsList()[count] = NewOrderItem;
                         return;
                     }
                }
            }
            count++;
        }
        throw new Exception("OrderItem cannot be found!");

    }
}