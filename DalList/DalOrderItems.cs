using DO;

namespace Dal;
public class DalOrderItems
{
    
    public void AddOrderItems()
    {
        int ProductId = 0;
        int.TryParse(Console.ReadLine(), out ProductId);
        int OrderItemId = 0;
        int.TryParse((Console.ReadLine()), out OrderItemId);
        double OrderItemPrice = 0;
        double.TryParse((Console.ReadLine()), out OrderItemPrice);
        int OrderItemAmount = 0;
        int.TryParse((Console.ReadLine()), out OrderItemAmount);
        OrderItems orderItems = new OrderItems();
        orderItems.ProductId = ProductId;
        orderItems.OrderId = OrderItemId;
        orderItems.Price = OrderItemPrice;
        orderItems.Amount = OrderItemAmount;
        DataSource.GetAddOrderItemToList(orderItems);
    }
   public OrderItems GetOrderItems(int OrderId,int ProductId)
   {
        OrderItems orderItems = new OrderItems();
        foreach (var item in DataSource._orderItems)
        {
            if(item.OrderId==OrderId && item.ProductId==ProductId)
                orderItems=item;
        }
    return orderItems;
   }
    public OrderItems[] GetOrderItemsList()
    {
        return DataSource._orderItems;
    }
    public void DeletOrderItem(int ProductId,int orderId)
    {
        int count = 0;
        foreach (var item in GetOrderItemsList())
        {
            if (item.OrderId == orderId && item.ProductId==ProductId)
            {
                for (int i = count; i < DataSource.Config._orderItemsIndex - 2; i++)// Shifting elements
                {
                    GetOrderItemsList()[i] = GetOrderItemsList()[i + 1];
                }
            }
            count++;

        }
    }
    /// <summary>
    /// /////////////////////////////////////////update//////////////////////////
    /// </summary>
    /// <param name="orderId"></param>
    public void UpdateOrderItem(int orderId, int productId)
    {
        int count = 0;
        foreach (var item in GetOrderItemsList())
        {
            if (item.OrderId == orderId && item.ProductId==productId )
            {
                OrderItems NewOrderItem = new OrderItems();
                NewOrderItem = GetOrderItems(orderId,productId);
                Console.WriteLine(@"choose option: 1. Update the Amount of your product
                                           2. PRESS ANY KEY TO THE EXIT OPTION");
                int choice = 0;
                int.TryParse(Console.ReadLine(), out choice);
                int c = 1;
                do // the customer can update maximum three parameter after that its exit of the function
                {
                    c++;
                    if (choice == 1)// update the name
                    {
                        DalProducts dalProducts = new DalProducts();
                        Console.WriteLine("Enter the new Amount: ");
                        int NewAmount = 0;
                        int.TryParse(Console.ReadLine(),out NewAmount);
                        NewOrderItem.Amount = NewAmount;
                        NewOrderItem.Price = NewAmount * dalProducts.GetProduct(productId).Price;
                    }
                   
                    else// exit 
                    {
                        Console.WriteLine("Order updated");
                        GetOrderItemsList()[count] = NewOrderItem;
                        return;
                    }
                }
                while (c <= 3);
            }
            count++;
        }
        throw new Exception("OrderItem cannot be found");

    }
}