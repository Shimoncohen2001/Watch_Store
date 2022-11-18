using DO;

namespace BO;

public class Cart
{
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAddress { get; set; }
    public OrderItems Items { get; set; }
    public double TotalPrice { get; set; }

    public override string ToString() => $@"
     Customer Name: {CustomerName}
     Customer Email: {CustomerEmail}
     Customer Address: {CustomerAddress}
     Order Item: {Items}
     TotalPrice: {TotalPrice}
    ";
        
}
