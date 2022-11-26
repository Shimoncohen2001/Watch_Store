using BlImplementation;
using BlApi;
namespace BlTest;

internal class Program
{
    public static void Display()
    {
        IBl bl = new Bl();
        BO.OrderItem orderItem = new BO.OrderItem();
        BO.Order order = new BO.Order();
        int flag = 1;
        while (flag != 0)
        {
            Console.WriteLine(@"Choose one of the several options: 0. Exit
                                   1. Test Products
                                   2. Test Orders
                                   3. Test OrderItem");
            int chosen = 0;
            int.TryParse(Console.ReadLine(), out chosen);
            switch (chosen)
            {
                case 0:
                    flag = 0;
                    Console.WriteLine("Thank you and Good Bye!");
                    break;
                case 1:
                    Console.WriteLine(@"Choose one of the several options: 1. Add a product
                                   2. Get a product for Director
                                   3. Get products List
                                   4. Update Product
                                   5. Delete Prodct
                                   6. Get a product for Client");
                    int chosen2 = 0;
                    int.TryParse(Console.ReadLine(), out chosen2);
                    switch (chosen2)
                    {
                        case 1:
                            BO.Product product = new BO.Product();
                            Console.WriteLine("Enter the id of your new product");
                            int id = 0;
                            int.TryParse(Console.ReadLine(), out id);
                            Console.WriteLine("Enter the name of your new product");
                            string name = Console.ReadLine();
                            Console.WriteLine("Enter the price of your new product");
                            double price = 0;
                            double.TryParse(Console.ReadLine(), out price);
                            Console.WriteLine("Choose the cathegory of your new product: 0.Men, 1. Women, 2. children");
                            string choice = Console.ReadLine();
                            BO.Category category = (BO.Category)Convert.ToInt32(choice);
                            Console.WriteLine("Enter the stock of your products");
                            int stock = 0;
                            int.TryParse(Console.ReadLine(), out stock);
                            product.ID = id;
                            product.Name = name;
                            product.Price = price;
                            product.InStock = stock;
                            product.Category = category;
                            bl.Product.Add(product);
                            break;
                        case 2:
                            Console.WriteLine("Enter the id of the product that you want: ");
                            int Newid = 0;
                            int.TryParse(Console.ReadLine(), out Newid);
                            Console.WriteLine(bl.Product.GetDirector(Newid).ToString());
                            break;
                        case 3:
                            foreach (var item in bl.Product.GetProductForLists())
                            {
                                Console.WriteLine(item.ToString());
                            }
                            break;
                        case 4:
                            BO.Product product1 = new BO.Product();
                            Console.WriteLine("Enter the id of the product you want update");
                            int id1 = 0;
                            int.TryParse(Console.ReadLine(), out id1);
                            Console.WriteLine("Enter the name of the product you want update");
                            string name1 = Console.ReadLine();
                            Console.WriteLine("Enter the price of the product you want update");
                            double price1 = 0;
                            double.TryParse(Console.ReadLine(), out price1);
                            Console.WriteLine("Enter the cathegory of the product you want update: 0.Men, 1. Women, 2. children");
                            string choice1 = Console.ReadLine();
                            BO.Category category1 = (BO.Category)Convert.ToInt32(choice1);
                            Console.WriteLine("Enter the stock of the product you want update");
                            int stock1 = 0;
                            int.TryParse(Console.ReadLine(), out stock1);
                            product1.ID = id1;
                            product1.Name = name1;
                            product1.Price = price1;
                            product1.Category = category1;
                            product1.InStock = stock1;
                            bl.Product.Update(product1);
                            break;
                        case 5:
                            Console.WriteLine("Enter the id of the product that you want to delete");
                            int id2 = 0;
                            int.TryParse(Console.ReadLine(), out id2);
                            bl.Product.Delete(id2);
                            break;
                        case 6:
                            Console.WriteLine("Enter the id of the product that you want:");
                            int id3 = 0;
                            int.TryParse(Console.ReadLine(), out id3);
                            Console.WriteLine("Enter your cart details:");
                            Console.Write("CustomerName: ");
                            string customerName = Console.ReadLine();
                            Console.Write("CustomerEmail: ");
                            string customerEmail = Console.ReadLine();
                            Console.Write("CustomerAddress: ");
                            string customerAddress = Console.ReadLine();
                            Console.Write("Items: ");
                            BO.OrderItem orderItem1 = new BO.OrderItem();
                            int orderItemId = 0;
                            int.TryParse(Console.ReadLine(), out orderItemId);
                            Console.Write("TotalPrice: ");
                            double totalPrice = 0;
                            double.TryParse(Console.ReadLine(), out totalPrice);
                            BO.Cart cart = new BO.Cart();
                            cart.CustomerName = customerName;
                            cart.CustomerEmail = customerEmail;
                            cart.CustomerAddress = customerAddress;
                            //Utiliser getOrderItem(orderItemId) pour remplir cart.Items
                            cart.TotalPrice = totalPrice;
                            bl.Product.GetClient(id3, cart);
                            break;
                        default:
                            break;
                    }
                    break;

                case 2:
                    Console.WriteLine(@"Choose one of the several options: 1. Get Order List
                                   2. Get an Order Item
                                   3. Update order to shipping status
                                   4. Update order to received status
                                   5. Track the order
                                   6. Update the Order");

                    int chosen3 = 0;
                    int.TryParse(Console.ReadLine(), out chosen3);
                    switch (chosen3)
                    {
                        case 1:
                            foreach (var item in bl.Order.GetOrderList())
                            {
                                Console.WriteLine(item.ToString());
                            }
                            break;
                        case 2:
                            Console.WriteLine("Enter the id of the Order you want");
                            int orderid = 0;
                            int.TryParse(Console.ReadLine(), out orderid);
                            Console.WriteLine(bl.Order.GetOrderItem(orderid).ToString());
                            break;
                        case 3:
                            Console.WriteLine("Enter the id of the Order you want update to shipping status");
                            int orderid1 = 0;
                            int.TryParse(Console.ReadLine(), out orderid1);
                            Console.WriteLine(bl.Order.UpdateOrderShipping(orderid1).ToString());
                            break;
                        case 4:
                            Console.WriteLine("Enter the id of the Order you want update to Received status");
                            int orderid2 = 0;
                            int.TryParse(Console.ReadLine(), out orderid2);
                            Console.WriteLine(bl.Order.UpdadteOrderReceived(orderid2).ToString());
                            break;
                        case 5:
                            Console.WriteLine("Enter the id of the Order you want update to Track");
                            int orderid3 = 0;
                            int.TryParse(Console.ReadLine(), out orderid3);
                            Console.WriteLine(bl.Order.TrackingOrder(orderid3).ToString());
                            break;
                        default:
                            break;
                    }
                    break;

                    case 3:
                     
                    Console.WriteLine(@"Choose one of the several options: 1. Add a Product to Cart
                                   2. Update amount of Product in Cart
                                   3. Cart confirmation ");
                    int chosen4 = 0;
                    int.TryParse(Console.ReadLine(), out chosen4);
                    switch (chosen4)
                    {
                        case 1:
                            Console.WriteLine("Enter the id of the product that you want:");
                            int id = 0;
                            int.TryParse(Console.ReadLine(), out id);
                            Console.WriteLine("Enter your name:");
                            string customerName = Console.ReadLine();
                            Console.Write("Enter the Customer Email: ");
                            string customerEmail = Console.ReadLine();
                            Console.Write("Enter the Customer Address: ");
                            string customerAddress = Console.ReadLine();
                            
                            BO.Cart cart = new BO.Cart();
                            cart.CustomerName = customerName;
                            cart.CustomerEmail = customerEmail;
                            cart.CustomerAddress = customerAddress;
                            //Utiliser getOrderItem(orderItemId1) pour remplir cart.Items
                            cart.TotalPrice =0;
                            bl.Cart.Add(cart, id);
                            break;

                        case 2:
                            Console.WriteLine("Enter the id of the product that you want:");
                            int id1 = 0;
                            int.TryParse(Console.ReadLine(), out id1);
                            Console.WriteLine("Enter the details of your Cart:");
                            Console.Write("Enter the Customer Name: ");
                            string customerName1 = Console.ReadLine();
                            Console.Write("Enter the Customer Email: ");
                            string customerEmail1 = Console.ReadLine();
                            Console.Write("Enter the Customer Address: ");
                            string customerAddress1 = Console.ReadLine();
                            BO.OrderItem orderItem2 = new BO.OrderItem();
                            int orderItemId2 = 0;
                            int.TryParse(Console.ReadLine(), out orderItemId2);
                            int totalPrice1 = 0;
                            int.TryParse(Console.ReadLine(), out totalPrice1);
                            BO.Cart cart1 = new BO.Cart();
                            cart1.CustomerName = customerName1;
                            cart1.CustomerEmail = customerEmail1;
                            cart1.CustomerAddress = customerAddress1;
                            //Utiliser getOrderItem(orderItemId2) pour remplir cart1.Items
                            cart1.TotalPrice = totalPrice1;
                            Console.WriteLine("Enter the new amount of product you choose in your cart:");
                            int newAmount = 0;
                            int.TryParse(Console.ReadLine(), out newAmount);
                            bl.Cart.Update(cart1, id1, newAmount);
                            break;

                        case 3:
                            // Confirmation cart func
                            break;
                        default:
                            break;
                            //}
                    }
                    break;
                default:
                    break;
            }
        }
    }
    static void Main(string[] args)
    {
        Display();
    }
}
