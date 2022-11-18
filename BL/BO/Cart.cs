using DO;

namespace BO;

public class Cart
{
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAddress { get; set; }
    public OrderItem Items { get; set; }  // De BL car ne peut pas posséder orderItem de DAL vu qu'on a aucune data ici
    public double TotalPrice { get; set; }

    public override string ToString() => $@"
     Customer Name: {CustomerName}
     Customer Email: {CustomerEmail}
     Customer Address: {CustomerAddress}
     Order Item: {Items}
     TotalPrice: {TotalPrice}
    ";
        
}
