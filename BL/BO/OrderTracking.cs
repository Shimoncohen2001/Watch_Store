using DO;

namespace BO;

public class OrderTracking
{
    public int ID { get; set; }
    public OrderStatus Status { get; set; }

    public override string ToString() => $@"
     Order ID: {ID}
     Order Status: {Status}"
    ;
}
