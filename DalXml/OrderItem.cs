using DAL;
using DalApi;
using DO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal;

internal class OrderItem : IOrderItem
{
    readonly string OrderItemPath = @"OrderItemXml.xml";

    public OrderItem()
    {
        if (!File.Exists(OrderItemPath))
            HelpXml.CreateFiles(OrderItemPath);
        else
            HelpXml.LoadData(OrderItemPath);
    }
    public void Add(OrderItems t)
    {
        List<OrderItems> ListOrderItems = HelpXml.LoadListFromXmlSerializer<OrderItems>(OrderItemPath);
        if (ListOrderItems.Any(c => c.Id == t.Id))
            throw new Exception("Order item already exists!");
        ListOrderItems.Add(t);
        HelpXml.SaveListToXmlSerializer(ListOrderItems, OrderItemPath);
    }

    public void Delete(int Id1, int Id2)
    {
        List<OrderItems> ListOrderItems = HelpXml.LoadListFromXmlSerializer<OrderItems>(OrderItemPath);
        DO.OrderItems orderItem = (from item in ListOrderItems
                                   where item.OrderId == Id1 && item.ProductId == Id2
                                   select item).FirstOrDefault();
        ListOrderItems.Remove(orderItem);
        HelpXml.SaveListToXmlSerializer(ListOrderItems, OrderItemPath);
    }

    public OrderItems Get(int Id1, int Id2)
    {
        List<OrderItems> ListOrderItems = HelpXml.LoadListFromXmlSerializer<OrderItems>(OrderItemPath);
        try
        {
            DO.OrderItems orderItem = (from item in ListOrderItems
                                       where item.OrderId == Id1 && item.ProductId == Id2
                                       select item).FirstOrDefault();

            return orderItem;
        }
        catch
        {
            throw new Exception("Order item doesn't exists");
        }
        
    }

    // A faire (même pb que pour order, il faut trouver comment faire avec le predicate)
    public OrderItems? GetItem(Func<OrderItems?, bool>? predicate)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<OrderItems?> GetList(Func<OrderItems?, bool>? predicate = null)
    {
        IEnumerable<DO.OrderItems?> ListOrderItems;
        ListOrderItems = HelpXml.LoadListFromXmlSerializer<DO.OrderItems?>(OrderItemPath).Where(predicate!);
        return ListOrderItems;
    }

    public void Update(int Id1, int Id2)
    {
        List<OrderItems> ListOrderItems = HelpXml.LoadListFromXmlSerializer<OrderItems>(OrderItemPath);
        DO.OrderItems orderItem = (from item in ListOrderItems
                                   where item.OrderId == Id1 && item.ProductId == Id2
                                   select item).FirstOrDefault();
        Console.WriteLine("Enter the orderId of the orderitem that you want: ");
        int orderId = 0;
        int.TryParse(Console.ReadLine(), out orderId);
        Console.WriteLine("Enter the productId of the orderitem that you want: ");
        int productId = 0;
        int.TryParse(Console.ReadLine(), out productId);
        Console.WriteLine("Enter the new amount: ");
        int Amount = 0;
        int.TryParse(Console.ReadLine(), out Amount);
        DO.OrderItems newOrderItem = new() { Id = orderItem.Id, OrderId= orderId, ProductId = productId, Amount = Amount, Price = ((double)Get(productId, 0).Price!) * Amount };
        orderItem = newOrderItem;
        HelpXml.SaveListToXmlSerializer(ListOrderItems, OrderItemPath);
    }
}
