using DO;
using DalApi;
using System.Dynamic;

namespace Dal;
internal class DalOrder:IOrder
{
    /// <summary>
    /// //////////////////////////Add/////////////////////////
    /// </summary>
    /// <param name="order"></param>
    public void Add(Order order) 
    {
        DataSource.GetAddOrderToList(order);
    }

    /// <summary>
    /// //////////////////////////GetById////////////////////
    /// </summary>
    /// <param name="OrderId"></param>
    /// <returns></returns>
    public Order Get(int OrderId, int val=0)
    {
        Order order = new Order();
        foreach (var item in    DataSource._orders)
        {
            if (item?.Id == OrderId)
            {
                order = (Order)item;
            }
        }
        return order;
    }

    /// <summary>
    /// //////////////////////GetList/////////////////////////
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Order?> GetList(Func<Order?,bool> func=null)
    {
        return DataSource._orders;
    }

    /// <summary>
    /// /////////////////////Delete///////////////////////////
    /// </summary>
    /// <param name="orderId"></param>
    public void Delete(int orderId,int v=0)
    {
        DataSource._orders.Remove(Get(orderId));
        DataSource.Config._automaticOrderId--;
    }

    /// <summary>
    /// //////////////////////////Update///////////////////////////////////
    /// </summary>
    /// <param name="orderId"></param>
    public void Update(int orderId,int v=0)
    {
        int count = 0;
        foreach (var item in GetList())
        {
            if (item?.Id == orderId)
            {
                Order NewOrder = new Order();
                NewOrder = Get(orderId);
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
                        GetList().ToList()[count] = NewOrder;// yoel propose modifff a revoir et tester
                        return;
                    }

                }
            }
                    count++;
        }
        throw new Exception("Order cannot be found!");
    }

    
}
