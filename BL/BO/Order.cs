using DO;

namespace BO;

public class Order
{
    public int ID { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public Category Category { get; set; }
    public int Amount { get; set; }
    public bool InStock { get; set; }

    public override string ToString() => $@"
     Order Id: {ID}
     Customer Name: {Name}
     Price: {Price}
     Category: {Category}
     Amount: {Amount}
     Instock: {InStock}
    ";
}
