
using System.Xml.Linq;

namespace DO;

public struct OrderItems
{
    public int ProductId { get; set; }  
    public int OrderId { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }
    public override string ToString() => $@"
    Order ID = {OrderId}: {ProductId},
    price - {Price}
    Amount: {Amount}
    ";
}
