using DO;
using DalApi;

namespace Dal;
internal class DalOrderItems : IOrderItem
{
    /// <summary>
    /// ////////////////////////////Add//////////////////////////
    /// </summary>
    /// <param name="orderItems"></param>
    public void Add(OrderItems orderItems)
    {
        DataSource.GetAddOrderItemToList(orderItems);
    }

    /// <summary>
    /// ///////////////////////////GetById//////////////////////
    /// </summary>
    /// <param name="OrderId"></param>
    /// <param name="ProductId"></param>
    /// <returns></returns>
    public OrderItems Get(int OrderId, int ProductId)
    {
        OrderItems orderItems = new OrderItems();
        foreach (var item in DataSource._orderItems)
        {
            if (item?.OrderId == OrderId && item?.ProductId == ProductId)
                orderItems = (OrderItems)item;
        }
        return orderItems;
    }

    /// <summary>
    /// ///////////////////////GetList/////////////////////////
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OrderItems?> GetList(Func<OrderItems?, bool> func=null)
    {
        if (func != null)
        {
            Predicate<OrderItems?> predicate1 = (ordI) => func(ordI);
            var newList = DataSource._orderItems.FindAll(predicate1);
            return newList;
        }
        return DataSource._orderItems;
    }

    /// <summary>
    /// //////////////////////Delete///////////////////////////
    /// </summary>
    /// <param name="ProductId"></param>
    /// <param name="orderId"></param>
    public void Delete(int ProductId, int orderId)
    {
        DataSource._orderItems.Remove(Get(orderId, ProductId));
    }

    /// <summary>
    /// //////////////////////////Update///////////////////////
    /// </summary>
    /// <param name="orderId"></param>
    public void Update(int orderId, int productId)
    {
        int count = 0;
        foreach (var item in GetList())
        {
            if (item?.OrderId == orderId && item?.ProductId == productId)
            {
                OrderItems NewOrderItem = new OrderItems();
                NewOrderItem = Get(orderId, productId);
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
                        NewOrderItem.Price = NewAmount * (double)(dalProducts.Get(productId).Price);
                    }
                    else // exit 
                    {
                        Console.WriteLine("Amount updated.");
                        GetList().ToList()[count] = NewOrderItem;
                        return;
                    }
                }
            }
            count++;
        }
        throw new Exception("OrderItem cannot be found!");

    }

}