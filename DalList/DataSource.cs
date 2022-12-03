using DO;
using System.Collections.Generic;
namespace Dal;
internal static class DataSource
{

    public static readonly int number;
    static Random rand = new Random();

    static DataSource()
    {
        number = rand.Next(100000, 999999);
        DataSource._Sinitialise();
    }

    internal static class Config
    {
        internal static int _automaticProdId { get; set; }
        internal static int _automaticOrderId { get; set; }
        internal static int _automaticOrderItemsId { get; set; }    
    }
    
    private static void _Sinitialise()
    {
        Config._automaticProdId = 100000;
        Config._automaticOrderId = 300;
        Config._automaticOrderItemsId = 0;
        ////////////////////////////Order///////////////////////////////////
        int i = 0;
        for ( ; i <= 20 ; i++)
        {
            int a = rand.Next(10);
            TimeSpan duration = new TimeSpan(0, a, 0, 0);
            Order ord = new Order();
            ord.Id = Config._automaticOrderId++;
            ord.CustomerName = ((CustomerNames)rand.Next(10)).ToString();
            ord.CustomerAdress = ((Adress)rand.Next(10)).ToString()+ " street, " + (rand.Next(50)).ToString();
            ord.CustomerEmail = ord.CustomerName + (rand.Next(200)).ToString() + "@gmail.com";
            ord.OrderDate = DateTime.Now;
           
            ord.ShipDate = ord.OrderDate.Add(duration);
            ord.DeliveryDate = ord.ShipDate.Add(duration);
            _orders.Add(ord); 
        }

        /////////////////////////Product///////////////////////////////////
        for (int a = 0; a <= 10; a++)
        {
            Products pro = new Products();
            pro.Id = Config._automaticProdId++;
            pro.Name = ((ProductName)rand.Next(10)).ToString();
            pro.Price = rand.Next(3000, 5000);
            pro.Category = (Category)rand.Next(3);
            int stock = 0;
            stock = rand.Next(10);
            pro.InStock = stock;
            _products.Add(pro);
        }

        ///////////////////////OrderItem///////////////////////////////////
        for (int b = 0; b < 40; b++)
        {
            OrderItems orderItems = new OrderItems();
            orderItems.Id = Config._automaticOrderItemsId++;
            orderItems.OrderId = rand.Next(300, 320);
            orderItems.ProductId = rand.Next(100000, 100010);
            orderItems.Amount = rand.Next(10);
            for(int c = 0; c < 10; c++)
            {
                if(_products[c].Id == orderItems.ProductId)
                {
                    orderItems.Price = (orderItems.Amount) * (_products[c].Price);
                }
            }
            _orderItems.Add(orderItems);
        }
    }

    // This will contain arrays for the "ישויות"
    // We nead to add a private function that will add all the objects in the array of each yechout
    internal static List<Order>_orders = new List<Order>(); // _camelCase
    internal static List<Products>_products = new List<Products>(); // _camelCase
    internal static List<OrderItems>_orderItems = new List<OrderItems>(); // _camelCase

    /// <summary>
    /// //////////////////////////////////Add//////////////////////////////////////////
    /// </summary>
    private static void AddOrderToList(Order o)  // add an order into the list of orders
    {
        if (_orders.Exists(p => p.Id == o.Id))
        {
            // exception already exist

        }
        else
        {
        Order order = new Order();  // create a new object to add a new element on the list of Orders
        order.Id = Config._automaticOrderId++;
        order.CustomerName = o.CustomerName;
        order.CustomerEmail = o.CustomerEmail;
        order.CustomerAdress = o.CustomerAdress;
        order.OrderDate = o.OrderDate;
        order.ShipDate = o.ShipDate;
        order.DeliveryDate = o.DeliveryDate;
        _orders.Add(order);  // add order into array in the first free place
        }

    }

    /// <summary>
    /// ////////////////////////////Add////////////////////////////////
    /// </summary>
    /// <param name="pr"></param>
    private static void AddProductToList(Products pr)  // Add an Product into the list of Products
    {
        if (_products.Exists(p => p.Id == pr.Id))
        {
            int index = _products.FindIndex(p => p.Id == pr.Id);
            Products product = new Products();
            product = _products[index];
            product.InStock += pr.InStock;
            _products[index] = product;
        }
        else
        {
            Products p = new Products();
            p.Id = pr.Id;
            p.Name = pr.Name;
            p.Price = pr.Price;
            p.Category = pr.Category;
            p.InStock = pr.InStock;
            _products.Add(p);  // Add product into array in the first free place
        }
    }

    /// <summary>
    /// ////////////////////////////////Add//////////////////////////////
    /// </summary>
    /// <param name="ordIt"></param>
    private static void AddOrderItemToList(OrderItems ordIt)  // Add an orderItem into the list of ordersItems
    {
        if (_orderItems.Exists(p =>p.ProductId == ordIt.ProductId && p.OrderId==ordIt.OrderId))
        {
            int index = _orderItems.FindIndex(p => p.OrderId == ordIt.OrderId && p.ProductId== ordIt.OrderId);
            OrderItems orderItems = new OrderItems();
            orderItems = _orderItems[index];
            orderItems.Amount += ordIt.Amount;
            _orderItems[index] = orderItems;
        }
        else
        {
            OrderItems Oitems = new OrderItems();
            Oitems.Id = Config._automaticOrderItemsId++;
            Oitems.ProductId = ordIt.ProductId;
            Oitems.OrderId = ordIt.OrderId;
            Oitems.Price = ordIt.Price;
            Oitems.Amount=ordIt.Amount;
            _orderItems.Add(Oitems);  // Add product into array in the first free place
        }
    }
    public static void GetAddOrderItemToList(OrderItems ordIt)
    {
        AddOrderItemToList(ordIt);
    }
            
    public static void GetAddOrderToList(Order ord)
    {
        AddOrderToList(ord);
    }
    public static void GetAddProductToList(Products prod)
    {
        AddProductToList(prod);
    }

}
