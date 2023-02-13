using DAL;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal;

internal class Order : IOrder
{
    public string OrderPath;
    public Order()
    {
        string localPath;
        string str = Assembly.GetExecutingAssembly().Location;
        localPath = Path.GetDirectoryName(str);
        localPath = Path.GetDirectoryName(localPath);
        //localPath = Path.GetDirectoryName(localPath);

        localPath += @"\xml";
        string extOrderPath = localPath + @"\OrderXml.xml";
        if (!File.Exists(extOrderPath))
        {
            HelpXml.CreateFiles(extOrderPath);
        }
        else
        {
            HelpXml.LoadData(extOrderPath);
        }
        OrderPath = extOrderPath;
    }

    public void Add(DO.Order t)
    {
        List<DO.Order> ListOrders = HelpXml.LoadListFromXmlSerializer<DO.Order>(OrderPath);
        if (ListOrders.Any(c => c.Id == t.Id))
            throw new Exception($"The customer ID {t.Id} exists already in the data!!");
        ListOrders.Add(t);
        HelpXml.SaveListToXmlSerializer(ListOrders, OrderPath);
    }

    public void Delete(int Id1, int Id2)
    {
        List<DO.Order> orders = HelpXml.LoadListFromXmlSerializer<DO.Order>(OrderPath);
        DO.Order order = (from item in orders
                          where item.Id == Id1
                          select item).FirstOrDefault();
        orders.Remove(order);
        HelpXml.SaveListToXmlSerializer(orders, OrderPath);
    }

    public DO.Order Get(int Id1, int Id2)
    {
        HelpXml.LoadData(OrderPath);
        DO.Order? order;
        IEnumerable<DO.Order?> orders = HelpXml.LoadListFromXmlSerializer<DO.Order?>(OrderPath);
        try
        {
            order = (from item in orders
                     where item?.Id == Id1
                     select item).FirstOrDefault();
            return (DO.Order)order!;
        }
        catch
        {
            throw new Exception("Order doesn't exist!");
        }
    }

    // Voir comment faire avec le predicate
    public DO.Order? GetItem(Func<DO.Order?, bool>? predicate)
    {
        //Predicate<DO.Order?> predicate1 = ord => predicate!(ord);
        //DO.Order? o = (from item in HelpXml.LoadListFromXmlSerializer<DO.Order?>(OrderPath)
        //               where HelpXml.LoadListFromXmlSerializer<DO.Order?>(OrderPath).Find(predicate1)
        //               select item);
        //return o;
        throw new NotImplementedException();
    }

    public IEnumerable<DO.Order?> GetList(Func<DO.Order?, bool>? predicate = null)
    {
        //IEnumerable<DO.Order?> listOrders;
        var listOrders = HelpXml.LoadListFromXmlSerializer<DO.Order?>(OrderPath);
        if (predicate!=null)
        {
            var listOrders1 = HelpXml.LoadListFromXmlSerializer<DO.Order?>(OrderPath).Where(predicate!);
            return listOrders1;
        }
        return listOrders;
    }


    public void Update(int Id1, int Id2)
    {
        List<DO.Order> listOrders = HelpXml.LoadListFromXmlSerializer<DO.Order>(OrderPath);
        DO.Order order = (from item in listOrders
                          where item.Id == Id1
                          select item).FirstOrDefault();
        Console.WriteLine("Enter the Customer Name: ");
        string name = Console.ReadLine()!;
        Console.WriteLine("Enter customer Email: ");
        string Email = Console.ReadLine()!;
        Console.WriteLine("Enter Customer adress: ");
        string Adress = Console.ReadLine()!;
        DO.Order newOrder = new() { Id = Id1 , CustomerName = name, CustomerEmail = Email, CustomerAdress = Adress, OrderDate = order.OrderDate, ShipDate = order.ShipDate, DeliveryDate = order.DeliveryDate};
        order = newOrder;
        HelpXml.SaveListToXmlSerializer(listOrders, OrderPath);
    }
}
