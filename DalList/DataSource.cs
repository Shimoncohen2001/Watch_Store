using DO;

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
        internal static int _orderIndex = 0;
        internal static int _productsIndex = 0;
        internal static int _orderItemsIndex = 0;
        internal static int _automaticProdId { get; set; }
        internal static int _automaticOrderId { get; set; }
    }
    
    private static void _Sinitialise()
    {
        Config._automaticProdId = 100000;
        Config._automaticOrderId = 300;

        ////////////////////////////Order///////////////////////////////////
        int i = 0;
        for ( ; i <= 20 ; i++)
        {
            Order ord = new Order();
            ord.Id = Config._automaticOrderId++;
            ord.CustomerName = ((CustomerNames)rand.Next(10)).ToString();
            ord.CustomerAdress = ((Adress)rand.Next(10)).ToString()+ " street, " + (rand.Next(50)).ToString();
            ord.CustomerEmail = ord.CustomerName + (rand.Next(200)).ToString() + "@gmail.com";
            ord.OrderDate = DateTime.Now;
            ord.ShipDate = DateTime.Now;
            ord.DeliveryDate = DateTime.Now;
            _orders[i] = ord; 
            Config._orderIndex++;
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
            _products[a] = pro;
            Config._productsIndex++;
        }

        ///////////////////////OrderItem///////////////////////////////////
        for (int b = 0; b < 40; b++)
        {
            OrderItems orderItems = new OrderItems();
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
            _orderItems[b] = orderItems;
            Config._orderItemsIndex++;
        }
    }

    // This will contain arrays for the "ישויות"
    // We nead to add a private function that will add all the objects in the array of each yechout
    internal static Order[]_orders = new Order[100]; // _camelCase
    internal static Products[]_products = new Products[50]; // _camelCase
    internal static OrderItems[]_orderItems = new OrderItems[200]; // _camelCase

    /// <summary>
    /// //////////////////////////////////Add//////////////////////////////////////////
    /// </summary>
    private static void AddOrderToList(Order o)  // add an order into the list of orders
    {
        //bool isExist = _orders.Any(order => order.Id == o.Id);
        //if (isExist)
        //{
        //    throw new Exception("this Order Id already exist");

        //}

        Order order = new Order();  // create a new object to add a new element on the list of Orders
        order.Id = o.Id;
        order.CustomerName = o.CustomerName;
        order.CustomerEmail = o.CustomerEmail;
        order.CustomerAdress = o.CustomerAdress;
        order.OrderDate = o.OrderDate;
        order.ShipDate = o.ShipDate;
        order.DeliveryDate = o.DeliveryDate;
        _orders[Config._orderIndex] = order;  // add order into array in the first free place
        Config._orderIndex++;

    }

    /// <summary>
    /// ////////////////////////////Add////////////////////////////////
    /// </summary>
    /// <param name="pr"></param>
    private static void AddProductToList(Products pr)  // Add an Product into the list of Products
    {
        int count = 0;
        bool exist = false;
        foreach (var item in _products)
        {
            if (pr.Id == item.Id)  // If product already existe only apdate the stock
            {
                exist=true;
                _products[count].InStock += pr.InStock;
            }
             count++;
        }
        
        if (!exist)
        {
            Products p = new Products();
            p.Id = pr.Id;
            p.Name = pr.Name;
            p.Price = pr.Price;
            p.Category = pr.Category;
            p.InStock = pr.InStock;
            _products[Config._productsIndex] = p;  // Add product into array in the first free place
            Config._productsIndex++;
        }
    }

    /// <summary>
    /// ////////////////////////////////Add//////////////////////////////
    /// </summary>
    /// <param name="ordIt"></param>
    private static void AddOrderItemToList(OrderItems ordIt)  // Add an orderItem into the list of ordersItems
    { 
        int count = 0;
        bool exist = false;
        foreach (var item in _orderItems)
        {
            if(item.OrderId == ordIt.OrderId)
            {
                exist = true;
                _orderItems[count].Amount = ordIt.Amount;
            }
            
            count++;
        }

        if(!exist)
        {

            OrderItems oi = new OrderItems();
            oi.ProductId = ordIt.ProductId;
            oi.OrderId = ordIt.OrderId;
            oi.Price = ordIt.Price;
            oi.Amount = ordIt.Amount;
            _orderItems[Config._orderItemsIndex] = oi;  // Add OrderItem into array in the first free place
            Config._orderItemsIndex++;
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
