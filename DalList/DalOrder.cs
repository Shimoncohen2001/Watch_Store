using DO;
namespace Dal;
public class DalOrder
{
    /// <summary>
    /// //////////////////////////Add/////////////////////////
    /// </summary>
    /// <param name="order"></param>
    public void AddOrder(Order order) 
    {
        DataSource.GetAddOrderToList(order);
    }

    /// <summary>
    /// //////////////////////////GetById////////////////////
    /// </summary>
    /// <param name="OrderId"></param>
    /// <returns></returns>
    public Order GetOrder(int OrderId)
    {
        Order order = new Order();
        foreach (var item in    DataSource._orders)
        {
            if (item.Id == OrderId)
            {
                order = item;
            }
        }
        return order;
    }

    /// <summary>
    /// //////////////////////GetList/////////////////////////
    /// </summary>
    /// <returns></returns>
    public Order[] GetOrderList()
    {
        return DataSource._orders;
    }

    /// <summary>
    /// /////////////////////Delete///////////////////////////
    /// </summary>
    /// <param name="orderId"></param>
    public void DeleteOrder(int orderId)
    {
        int count = 0;
        foreach (var item in GetOrderList())
        {
            if (item.Id == orderId)
            {
                for (int i = count; i < DataSource.Config._orderIndex; i++) // Shifting elements
                {
                    GetOrderList()[i] = GetOrderList()[i + 1];
                }
            }
            count++;

        }
    }

    /// <summary>
    /// //////////////////////////Update///////////////////////////////////
    /// </summary>
    /// <param name="orderId"></param>
    public void UpdateOrder(int orderId)
    {
        int count = 0;
        foreach (var item in GetOrderList())
        {
            if (item.Id == orderId)
            {
                Order NewOrder = new Order();
                NewOrder = GetOrder(orderId);
                bool finish=false;
                while (!finish)
                {
                    Console.WriteLine(@"choose option: 
                                           1. Update the name Customer
                                           2. Update the Email Customner
                                           3. Update the adress customer
                                           4. EXIT");
                    int choice = 0;
                    int.TryParse(Console.ReadLine(), out choice);
                    if (choice == 1)// update the name
                    {
                        Console.WriteLine("Enter the new Name: ");
                        string NewName = Console.ReadLine();
                        NewOrder.CustomerName = NewName;
                    }
                    else if (choice == 2)// update the Email
                    {
                        Console.WriteLine("Enter the new Email: ");
                        string NewEmail = Console.ReadLine();
                        NewOrder.CustomerEmail = NewEmail;
                    }
                    else if (choice == 3)// uodate the adress
                    {
                        Console.WriteLine("Enter the new Adress: ");
                        string NewAdress = Console.ReadLine();
                        NewOrder.CustomerAdress = NewAdress;
                    }
                    else// exit 
                    {
                        Console.WriteLine("Order updated.");
                        GetOrderList()[count] = NewOrder;
                        return;
                    }

                }
            }
                    count++;
        }
        throw new Exception("Order cannot be found!");

    }
}
