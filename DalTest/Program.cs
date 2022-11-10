using System;
using Dal;
using DalFacade;
namespace DO;
static class  Program
{ 
    public static void Display()
    {
                     DalProducts products=new DalProducts();
                     DalOrder dalOrder=new DalOrder();  
                     DalOrderItems dalOrderItems = new DalOrderItems();
        int flag=1;
        while(flag!=0)
        {
            Console.WriteLine(@"choose one of the several options:
                                0. Exit, 1. Test Products, 2. Test Orders, 3. Test OrderItem");
            int chosen = 0;
            int.TryParse(Console.ReadLine(), out chosen);
            switch (chosen)
            {
                case 0:
                    flag = 0;
                    Console.WriteLine("Thank you and Good Bye!");
                    break;
                case 1:
                    Console.WriteLine(@"Choose one of the several options:
                                        1. Add a product, 2. Get a product,3. Get products List,4. Update Product,5. Delete Prodct ");
                    int chosen2 = 0;
                    int.TryParse(Console.ReadLine(), out chosen2);
                    switch (chosen2)
                    {
                        case 1:
                            // mettre le pelet des fomctions add
                            Console.WriteLine("enter the id of your new product");
                            int id = 0;
                            int.TryParse(Console.ReadLine(), out id );
                            Console.WriteLine("Enter the name of your new product");
                            string name=Console.ReadLine();
                            Console.WriteLine("enter the price of your new product");
                            double price = 0;
                            double.TryParse(Console.ReadLine(), out price );
                            Console.WriteLine("choose the cathegory of your new product: 1.Men, 2. Women, 3. children");
                            string choice=Console.ReadLine();
                            Category category=(Category)Convert.ToInt32(choice);
                            Console.WriteLine("enter the stock of your products");
                            int stock = 0;
                            int.TryParse(Console.ReadLine(),out stock);
                            Products p = new Products();
                            p.Name = name;
                            p.Id=id;
                            p.Price = price;
                            p.InStock = stock;
                            p.Category=category;
                            products.AddProducts(p);
                            break;
                        case 2:
                            Console.WriteLine("Enter the id of the product that you want: ");
                            int Newid = 0;
                            int.TryParse(Console.ReadLine(),out Newid);
                            Console.WriteLine(products.GetProduct(Newid).ToString());
                            break;
                        case 3:
                            foreach (var item in products.GetProductList())
                            {
                                if(item.Id!=0)
                                {
                                 Console.WriteLine(item.ToString());
                                }
                            }
                            break;
                        case 4:
                            Console.WriteLine("enter the id of the product that you want to update");
                            int id2 = 0;
                            int.TryParse(Console.ReadLine(), out id2);
                            // toute les saisis du update doivent etre dans le main
                            products.UpdateProduct(id2);
                            break;
                        case 5:
                            Console.WriteLine("enter the id of the product that you want to delete");
                            int id3 = 0;
                            int.TryParse(Console.ReadLine(), out id3);
                            products.DeletProduct(id3);
                            break;
                        default:
                            break;
                    }
                    break;
                case 2:
                    Console.WriteLine(@"Choose one of the several options:
                                        1. Add an Order, 2. Get an Order,3. Get order List,4. Update order,5. Delete order ");
                    int chosen3 = 0;
                    int.TryParse(Console.ReadLine(),out chosen3);
                    switch (chosen3)
                    {
                        case 1:
                            // mettre le pelet des order
                            Console.WriteLine("Enter an Id: ");
                            int OrderId = 0;
                            int.TryParse(Console.ReadLine(),out OrderId);
                            Console.WriteLine("enter the CustomerName: ");
                            string name=Console.ReadLine();
                            Console.WriteLine("Enter customer Email");
                            string Email=Console.ReadLine();
                            Console.WriteLine("Enter Customer adress");
                            string Adress=Console.ReadLine();
                            DateTime orderDate=DateTime.Now;
                            DateTime shipdate=DateTime.Now;
                            DateTime deliveryDate=DateTime.Now;
                            Order ord=new Order();
                            ord.OrderDate=orderDate;
                            ord.ShipDate=shipdate;
                            ord.DeliveryDate=deliveryDate;
                            ord.CustomerName = name;
                            ord.CustomerEmail = Email;
                            ord.CustomerAdress = Adress;
                            dalOrder.AddOrder(ord);
                            break;
                        case 2:
                            Console.WriteLine("enter the id of the order that you want: ");
                            int orderid=0;
                            int.TryParse(Console.ReadLine(),out orderid);
                            Console.WriteLine(dalOrder.GetOrder(orderid));
                            break;
                        case 3:
                            foreach (var item in dalOrder.GetOrderList())
                            {
                                if(item.Id!=0)
                                {
                                Console.WriteLine(item.ToString());
                                }
                            }
                            break;
                        case 4:
                            Console.WriteLine("enter the id of the product that you want to update");
                            int orderid2 = 0;
                            int.TryParse(Console.ReadLine(), out orderid2);
                            dalOrder.UpdateOrder(orderid2);
                            break;
                        case 5:
                            Console.WriteLine("enter the id that you want to delete");
                            int orderid3 = 0;
                            int.TryParse(Console.ReadLine(), out orderid3);
                            dalOrder.DeleteOrder(orderid3);
                            break;
                            
                        default:
                            break;
                    }
                    break;
                case 3:
                    Console.WriteLine(@"Choose one of the several options:
                                        1. Add a Orderitem, 2. Get a orderitem,3. Get orderitem List,4. Update orderitem,5. Delete orderitem ");
                    int chosen4 = 0;
                    int.TryParse(Console.ReadLine(), out chosen4);
                    switch (chosen4)
                    {
                        case 1:
                            Console.WriteLine("enter the OrderId: ");
                            int ordId=0;
                            int.TryParse(Console.ReadLine(),out ordId);
                            Console.WriteLine("Enter the prodId: ");
                            int productId = 0;
                            int.TryParse(Console.ReadLine(),out productId);
                            int Amount = 0;
                            int.TryParse(Console.ReadLine(),out Amount);
                            OrderItems orderitems=new OrderItems();
                            orderitems.OrderId = ordId;
                            orderitems.ProductId = productId;
                            orderitems.Amount = Amount;
                            orderitems.Price = (products.GetProduct(productId).Price) * Amount;
                            dalOrderItems.AddOrderItems(orderitems);
                            break;
                        case 2:
                            Console.WriteLine("enter the order id of the orderitem that you want: ");
                            int orderid = 0;
                            int.TryParse(Console.ReadLine(), out orderid);
                            Console.WriteLine("enter the product id of the orderitem that you want: ");
                            int  prodId=0;
                            int.TryParse(Console.ReadLine(), out prodId);
                            dalOrderItems.GetOrderItems(orderid, prodId);
                            break;
                        case 3:
                            foreach (var item in dalOrderItems.GetOrderItemsList())
                            {
                                if(item.ProductId!=0&&item.OrderId!=0)
                                {
                                Console.WriteLine(item.ToString());
                                }
                            }
                            break;
                        case 4:
                            Console.WriteLine("Enter the orderId of the orderitem that you want: ");
                            int orderid2 = 0;
                            int.TryParse(Console.ReadLine(), out orderid2);
                            Console.WriteLine("Enter the prodid of the orderitem that you want: ");
                            int prodId2 = 0;
                            int.TryParse(Console.ReadLine(),out prodId2);
                            dalOrderItems.UpdateOrderItem(orderid2,prodId2);
                            break;
                        case 5:
                            Console.WriteLine("Enter the orderId of the orderitem that you want to delete: ");
                            int orderid3 = 0;
                            int.TryParse(Console.ReadLine(), out orderid3);
                            Console.WriteLine("Enter the prodid of the orderitem that you want to delete: ");
                            int prodId3 = 0;
                            int.TryParse(Console.ReadLine(), out prodId3);
                            dalOrderItems.DeletOrderItem(orderid3, prodId3);
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }


        }
    }
    
    public static void Main(string[]args)
    {
        Display();
    }

}
