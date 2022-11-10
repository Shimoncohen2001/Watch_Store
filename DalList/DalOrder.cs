using DO;
namespace Dal;
public class DalOrder
{
    public void AddOrder(Order order)// ne pas oublier de return le id de ce qu'on a ajoutter
    {
        DataSource.GetAddOrderToList(order);
    }

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
    public Order[] GetOrderList()
    {
        return DataSource._orders;
    }
    public void DeleteOrder(int orderId)
    {
        int count = 0;
        foreach (var item in GetOrderList())
        {
            if (item.Id == orderId)
            {
                for (int i = count; i < DataSource.Config._orderIndex - 2; i++)// Shifting elements
                {
                    GetOrderList()[i] = GetOrderList()[i + 1];
                }
            }
            count++;

        }
    }
    /// <summary>
    /// //////////////////////////update///////////////////////////////////
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
                                           2. update the Email Customner
                                           3. update the adress customer
                                           4. PRESS ANY KEY TO THE EXIT OPTION");
                    int choice = 0;
                    int.TryParse(Console.ReadLine(), out choice);
                    if (choice == 1)// update the name
                    {
                        Console.WriteLine("Enter the new name: ");
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
                        Console.WriteLine("Order updated");
                        GetOrderList()[count] = NewOrder;
                        return;
                    }

                }
            }
                    count++;
        }
        throw new Exception("Order cannot be found");

    }
}
