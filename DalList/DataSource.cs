using DO;

namespace Dal;
internal static class DataSource
{
    public static readonly int number;
    static Random rand = new Random();
    static DataSource()
    {
        number = rand.Next(111111111,999999999);
        DataSource._Sinitialise(number);
    }
    internal static class Config
    {
        internal static int _orderIndex=0;
        internal static int _productsIndex=0;
        internal static int _orderItemsIndex=0;
        internal static int _automaticId { get=>_automaticId; set=>_automaticId=100000; }
    }
    
    private static void _Sinitialise(int value)
    {
        ////////////////////////////Order///////////////////////////////////
        int i = 0;
        for ( i = 0; i <=20 ; i++)
        {
            Order ord=new Order();
            ord.Id = value;
            ord.CustomerName=((CustomerNames)rand.Next(10)).ToString();
            ord.CustomerAdress= ((Adress)rand.Next(10)).ToString()+"street"+ (rand.Next(50)).ToString();
            ord.CustomerEmail = _orders[i].CustomerName + (rand.Next(200)).ToString() + "@gmail.com";
            ord.OrderDate= DateTime.Now;
            ord.ShipDate= DateTime.Now;
            ord.DeliveryDate= DateTime.Now;
            AddOrderToList(ord);
        }
            DataSource.Config._orderIndex=i++;
        /////////////////////////Product///////////////////////////////////
        for (int a = 0; a <=10; a++)
        {
            Products pro=new Products();
            pro.Id =Config._automaticId++;
            pro.Name = ((ProductName)rand.Next(6)).ToString();
            //_products[a].Price= 
            // pro.price a faire
            pro.Category = (Category)rand.Next(3);
            // pro.instock a faire
            int stock = 0;
            stock=rand.Next(10);
            // appeler la fonction update  product et la bas modifier la fonction
            // if there is also on the stock this kind of product we need to additionate the new amount to the previous one
            AddProductToList(pro);
        }
        ///////////////////////orderItem///////////////////////////////////
        for(int a = 0; a <=40; a++)
        {

            OrderItems ordI=new OrderItems();
            ordI.ProductId = _products[a].Id;
            ordI.OrderId = _orders[a].Id;
            int amount = 0;
            amount=rand.Next(10);
            ordI.Amount = amount;
            ordI.Price = (_products[a].Price)*amount;
            // gerer le prix
            //ne pas oublier d'appeler la fonction update pour mettre a jour les valeurs
            AddOrderItemToList(ordI);
        }
    }
    // this will contain arrays for the "ישויות"
    // we nead to add a private function that will add all the objects in the array of each yechout
    internal static Order[]_orders =new Order[100];// _camelCase
    internal static Products[]_products =new Products[50];// _camelCase
    internal static OrderItems[]_orderItems = new OrderItems[200];// _camelCase

    /// <summary>
    /// //////////////////////////////////add//////////////////////////////////////////
    /// </summary>
    private static void AddOrderToList(Order o)// add an order into the list of orders
    {
        //int Orderid = 0;
        //int.TryParse(Console.ReadLine(), out Orderid);
        //string Customername=Console.ReadLine();
        //string Customeraddress=Console.ReadLine(); 
        //string CustomerEmail=Console.ReadLine();
        //DateTime Orderderdate = DateTime.Now;
        //DateTime Shipdate=DateTime.Now;
        //DateTime DeliveryDate=DateTime.Now; 


        Order order=new Order();// create a new object to add a new element on the list of Orders
        order.Id = o.Id;
        order.CustomerName = o.CustomerName;
        order.CustomerEmail = o.CustomerEmail;
        order.CustomerAdress = o.CustomerAdress;
        order.OrderDate = o.OrderDate; 
        order.ShipDate= o.ShipDate;
        order.DeliveryDate = o.DeliveryDate;  
        _orders[Config._orderIndex] = order;// add order into array in the first free place
        Config._orderIndex++;
        
    }
    private static void AddProductToList(Products pr)// add an Product into the list of Products
    {
        //int ProductId = 0;
        //int.TryParse(Console.ReadLine(), out ProductId);
        //string ProductName = Console.ReadLine();
        //double ProductPrice = 0;
        //double.TryParse(Console.ReadLine(), out ProductPrice);
        //Console.WriteLine("Enter the categorie 0:Men 1: women 2: children");
        //string chosen = Console.ReadLine();
        //Category category = new Category();
        //category=(Category)Convert.ToInt32(chosen);
        //int ProductInStock = 0;
        //int.TryParse(Console.ReadLine(), out ProductInStock);
        Products p=new Products();
        p.Id= pr.Id;
        p.Name = pr.Name;
        p.Price= pr.Price; 
        p.Category=pr.Category;
        p.InStock= pr.InStock;
        _products[Config._productsIndex] = p;// add product into array in the first free place
        Config._productsIndex++;
    }
    private static void AddOrderItemToList(OrderItems ordIt)// add an orderItem into the list of ordersItems
    {

        //int ProductId = 0;
        //int.TryParse(Console.ReadLine(), out ProductId);
        //int OrderItemId = 0;
        //int.TryParse((Console.ReadLine()), out OrderItemId); 
        //double OrderItemPrice = 0;  
        //double.TryParse((Console.ReadLine()), out OrderItemPrice);
        //int OrderItemAmount = 0;
        //int.TryParse((Console.ReadLine()), out OrderItemAmount);

        OrderItems oi= new OrderItems();
        oi.ProductId = ordIt.ProductId;
        oi.OrderId = ordIt.OrderId;
        oi.Price = ordIt.Price;
        oi.Amount = ordIt.Amount;
        _orderItems[Config._orderItemsIndex] = oi;// add OrderItem into array in the first free place
        Config._orderItemsIndex++;
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
