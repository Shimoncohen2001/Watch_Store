using DO;

namespace BO;

public class Order
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAdress { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime PaymentDate { get; set; }
    public DateTime ShipDate { get; set; }
    public DateTime DeliveryDate { get; set; }

    public List<OrderItem> orderItems=new List<OrderItem>();
    //public OrderItem Items { get; set; } // De BL car ne peut pas posséder orderItem de DAL vu qu'on a aucune data ici
    public double TotalPrice { get; set; }

    public override string ToString() => $@"
     Order Id: {Id}
     Customer Name: {CustomerName}
     CustomerEmail: {CustomerEmail}
     CustomerAdress: {CustomerAdress}
     Order Date: {OrderDate}
     Order Status: {Status}
     Payment Date: {PaymentDate}
     Ship Date: {ShipDate}
     DeliveryDate: {DeliveryDate}
     Order Item: {orderItems}
     Toal Price: {TotalPrice}
    ";
}
