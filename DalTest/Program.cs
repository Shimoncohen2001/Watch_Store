using Dal;
using DalApi;
using System;
using System.Collections.Immutable;

namespace DO;
static class Program
{
    public static void Display()
    {
        IDal dal = new DalList();
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
                                   2. Get a product
                                   3. Get products List
                                   4. Update Product
                                   5. Delete Prodct ");

                    int chosen2 = 0;
                    int.TryParse(Console.ReadLine(), out chosen2);
                    switch (chosen2)
                    {
                        case 1:
                            // mettre le pelet des fomctions add
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
                            Category category = (Category)Convert.ToInt32(choice);
                            Console.WriteLine("Enter the stock of your products");
                            int stock = 0;
                            int.TryParse(Console.ReadLine(), out stock);
                            Products p = new Products();
                            p.Name = name;
                            p.Id = id;
                            p.Price = price;
                            p.InStock = stock;
                            p.Category = category;
                            dal.Product.Add(p);
                            break;

                        case 2:
                            Console.WriteLine("Enter the id of the product that you want: ");
                            int Newid = 0;
                            int.TryParse(Console.ReadLine(), out Newid);
                            Console.WriteLine(dal.Product.Get(Newid, 0).ToString());
                            break;

                        case 3:
                            Console.WriteLine(@"Sort your list of product by: 1: All
                                2: Category
                                3:from most expensive to least expensive
                                4:From Least expensive to most Expensive    
                                5:By Brands
                                ");
                            int chosen5 = 0;
                            int.TryParse(Console.ReadLine(), out chosen5);
                            switch (chosen5)
                            {
                                case 1:
                                    foreach (var item in dal.Product.GetList())
                                    {
                                        if (item?.Id != 0)
                                        {
                                            Console.WriteLine(item.ToString());
                                        }
                                    }
                                    break;
                                case 2:
                                    Console.WriteLine("1: Men------------- 2: Women --------------3: Children-----------");
                                    int choice1 = 0;
                                    int.TryParse(Console.ReadLine(), out choice1);
                                    if (choice1 == 1)
                                    {
                                        Func<Products?, bool>? optionaldel = (Prod) => Prod?.Category == Category.Men;
                                        foreach (var item in dal.Product.GetList(optionaldel))
                                        {
                                            if (item?.Id != 0)
                                            {
                                                Console.WriteLine(item.ToString());
                                            }
                                        }
                                    }
                                    if (choice1 == 2)
                                    {
                                        Func<Products?, bool>? predicate = (Prod) => Prod?.Category == Category.Women;
                                        foreach (var item in dal.Product.GetList(predicate))
                                        {
                                            if (item?.Id != 0)
                                            {
                                                Console.WriteLine(item.ToString());
                                            }
                                        }
                                    }
                                    if (choice1 == 3)
                                    {
                                        Func<Products?, bool>? predicate1 = (Prod) => Prod?.Category == Category.Children;
                                        foreach (var item in dal.Product.GetList(predicate1))
                                        {
                                            if (item?.Id != 0)
                                            {
                                                Console.WriteLine(item.ToString());
                                            }
                                        }
                                    }
                                    break;
                                case 3:
                                    foreach (var item in dal.Product.GetList().OrderByDescending(product => product?.Price).ToList())
                                    {
                                        if (item?.Id != 0)
                                        {
                                            Console.WriteLine(item.ToString());
                                        }
                                    }
                                    break;
                                case 4:
                                    foreach (var item in dal.Product.GetList().OrderBy(product => product?.Price).ToList())
                                    {
                                        if (item?.Id != 0)
                                        {
                                            Console.WriteLine(item.ToString());
                                        }
                                    }
                                    break;
                                case 5:
                                    string prodName;
                                    prodName = Console.ReadLine();
                                    Func<Products?, bool>? predicate2 = (Prod) => Prod?.Name == prodName;
                                    foreach (var item in dal.Product.GetList(predicate2))
                                    {
                                        if (item?.Id != 0)
                                        {
                                            Console.WriteLine(item.ToString());
                                        }
                                    }
                                    break;
                                default:
                                    break;
                            }
                            break;

                        case 4:
                            Console.WriteLine("Enter the id of the product that you want to update");
                            int id2 = 0;
                            int.TryParse(Console.ReadLine(), out id2);
                            // toute les saisis du update doivent etre dans le main
                            dal.Product.Update(id2, 0);
                            break;

                        case 5:
                            Console.WriteLine("Enter the id of the product that you want to delete");
                            int id3 = 0;
                            int.TryParse(Console.ReadLine(), out id3);
                            dal.Product.Delete(id3, 0);
                            break;

                        default:
                            break;
                    }
                    break;

                case 2:
                    Console.WriteLine(@"Choose one of the several options: 1. Add an Order
                                   2. Get an Order
                                   3. Get order List
                                   4. Update order
                                   5. Delete order");

                    int chosen3 = 0;
                    int.TryParse(Console.ReadLine(), out chosen3);
                    TimeSpan duration = new TimeSpan(3, 0, 0, 0);

                    switch (chosen3)
                    {
                        case 1:
                            // mettre le pelet des order    
                            Console.WriteLine("Enter an Id: ");
                            int OrderId = 0;
                            int.TryParse(Console.ReadLine(), out OrderId);
                            Console.WriteLine("Enter the Customer Name: ");
                            string name = Console.ReadLine();
                            Console.WriteLine("Enter customer Email: ");
                            string Email = Console.ReadLine();
                            Console.WriteLine("Enter Customer adress: ");
                            string Adress = Console.ReadLine();
                            DateTime orderDate = DateTime.Now;
                            DateTime shipdate = orderDate.Add(duration);
                            DateTime deliveryDate = shipdate.Add(duration);
                            Order ord = new Order();
                            ord.Id = OrderId;
                            ord.OrderDate = orderDate;
                            ord.ShipDate = shipdate;
                            ord.DeliveryDate = deliveryDate;
                            ord.CustomerName = name;
                            ord.CustomerEmail = Email;
                            ord.CustomerAdress = Adress;
                            dal.Order.Add(ord);
                            break;

                        case 2:
                            Console.WriteLine("Enter the id of the order that you want: ");
                            int orderid = 0;
                            int.TryParse(Console.ReadLine(), out orderid);
                            Console.WriteLine(dal.Order.Get(orderid, 0));
                            break;

                        case 3:
                            Console.WriteLine(@"Sort your list of order by: 1: All
                                2: By CustomerName 
                                3: By Date
                                ");
                            int chosen5 = 0;
                            int.TryParse(Console.ReadLine(), out chosen5);
                            switch (chosen5)
                            {
                                case 1:
                                    foreach (var item in dal.Order.GetList())
                                    {
                                        if (item?.Id != 0)
                                        {
                                            Console.WriteLine(item.ToString());
                                        }
                                    }
                                    break;
                                case 2:
                                    Console.WriteLine("1: CustomerName in order------------- 2: CustomerName Descending--------------");
                                    int choice1 = 0;
                                    int.TryParse(Console.ReadLine(), out choice1);
                                    if (choice1 == 1)
                                    {
                                        foreach (var item in dal.Order.GetList().OrderBy(order => order?.CustomerName).ToList())
                                        {
                                            if (item?.Id != null)
                                            {
                                                Console.WriteLine(item.ToString());
                                            }
                                        }
                                    }
                                    if (choice1 == 2)
                                    {
                                        foreach (var item in dal.Order.GetList().OrderByDescending(order => order?.CustomerName).ToList())
                                        {
                                            if (item?.Id != null)
                                            {
                                                Console.WriteLine(item.ToString());
                                            }
                                        }
                                    }
                                    break;
                                case 3:
                                    Console.WriteLine("1: OrderDate------------- 2: ShipDate-------------- 3: DeliveryDate-------------");
                                    int choice2 = 0;
                                    int.TryParse(Console.ReadLine(), out choice2);
                                    if (choice2 == 1)
                                    {
                                        foreach (var item in dal.Order.GetList().OrderBy(order => order?.OrderDate).ToList())
                                        {
                                            if (item?.Id != null)
                                            {
                                                Console.WriteLine(item.ToString());
                                            }
                                        }
                                    }
                                    if (choice2 == 2)
                                    {
                                        foreach (var item in dal.Order.GetList().OrderByDescending(order => order?.ShipDate).ToList())
                                        {
                                            if (item?.Id != null)
                                            {
                                                Console.WriteLine(item.ToString());
                                            }
                                        }
                                    }
                                    if (choice2 == 3)
                                    {
                                        foreach (var item in dal.Order.GetList().OrderByDescending(order => order?.DeliveryDate).ToList())
                                        {
                                            if (item?.Id != null)
                                            {
                                                Console.WriteLine(item.ToString());
                                            }
                                        }
                                    }
                                    break;

                                default:
                                    break;
                            }
                            break;

                        case 4:
                            Console.WriteLine("Enter the id of the product that you want to update:");
                            int orderid2 = 0;
                            int.TryParse(Console.ReadLine(), out orderid2);
                            dal.Order.Update(orderid2, 0);
                            break;

                        case 5:
                            Console.WriteLine("Enter the id that you want to delete:");
                            int orderid3 = 0;
                            int.TryParse(Console.ReadLine(), out orderid3);
                            dal.Order.Delete(orderid3, 0);
                            break;

                        default:
                            break;
                    }
                    break;

                case 3:
                    Console.WriteLine(@"Choose one of the several options: 1. Add a Orderitem
                                   2. Get a orderitem
                                   3. Get orderitem List
                                   4. Update orderitem
                                   5. Delete orderitem ");

                    int chosen4 = 0;
                    int.TryParse(Console.ReadLine(), out chosen4);
                    switch (chosen4)
                    {
                        case 1:
                            Console.WriteLine("enter the OrderId: ");
                            int ordId = 0;
                            int.TryParse(Console.ReadLine(), out ordId);
                            Console.WriteLine("Enter the prodId: ");
                            int productId = 0;
                            int.TryParse(Console.ReadLine(), out productId);
                            Console.WriteLine("Enter the amount: ");
                            int Amount = 0;
                            int.TryParse(Console.ReadLine(), out Amount);
                            OrderItems orderitems = new OrderItems();
                            orderitems.OrderId = ordId;
                            orderitems.ProductId = productId;
                            orderitems.Amount = Amount;
                            orderitems.Price = (dal.Product.Get(productId, 0).Price) * Amount;
                            dal.OrderItem.Add(orderitems);
                            break;

                        case 2:
                            Console.WriteLine("Enter the order id of the orderitem that you want: ");
                            int orderid = 0;
                            int.TryParse(Console.ReadLine(), out orderid);
                            Console.WriteLine("Enter the product id of the orderitem that you want: ");
                            int prodId = 0;
                            int.TryParse(Console.ReadLine(), out prodId);
                            Console.WriteLine(dal.OrderItem.Get(orderid, prodId));
                            break;

                        case 3:
                            Console.WriteLine(@"Sort your list of order by: 1: All
                                    2: By Price 
                                    3: By Amount
                                    ");
                            int chosen5 = 0;
                            int.TryParse(Console.ReadLine(), out chosen5);
                            switch (chosen5)
                            {
                                case 1:
                                    foreach (var item in dal.OrderItem.GetList())
                                    {
                                        if (item?.ProductId != 0 && item?.OrderId != 0)
                                        {
                                            Console.WriteLine(item.ToString());
                                        }
                                    }
                                    break;
                                case 2:
                                    Console.WriteLine("1: Ordered Price------------- 2: Descending Price--------------");
                                    int choice1 = 0;
                                    int.TryParse(Console.ReadLine(), out choice1);
                                    if (choice1 == 1)
                                    {
                                        foreach (var item in dal.OrderItem.GetList().OrderBy(orderItem => orderItem?.Price).ToList())
                                        {
                                            if (item?.Id != null)
                                            {
                                                Console.WriteLine(item.ToString());
                                            }
                                        }
                                    }
                                    if (choice1 == 2)
                                    {
                                        foreach (var item in dal.OrderItem.GetList().OrderByDescending(orderItem => orderItem?.Price).ToList())
                                        {
                                            if (item?.Id != null)
                                            {
                                                Console.WriteLine(item.ToString());
                                            }
                                        }
                                    }
                                    break;
                                case 3:
                                    Console.WriteLine("1: Ordered Amount------------- 2: Descending Amount--------------");
                                    int choice2 = 0;
                                    int.TryParse(Console.ReadLine(), out choice2);
                                    if (choice2 == 1)
                                    {
                                        foreach (var item in dal.OrderItem.GetList().OrderBy(orderItem => orderItem?.Amount).ToList())
                                        {
                                            if (item?.Id != null)
                                            {
                                                Console.WriteLine(item.ToString());
                                            }
                                        }
                                    }
                                    if (choice2 == 2)
                                    {
                                        foreach (var item in dal.OrderItem.GetList().OrderByDescending(orderItem => orderItem?.Amount).ToList())
                                        {
                                            if (item?.Id != null)
                                            {
                                                Console.WriteLine(item.ToString());
                                            }
                                        }
                                    }
                                    break;

                                default:
                                    break;
                            }
                            break;

                        case 4:
                            Console.WriteLine("Enter the orderId of the orderitem that you want: ");
                            int orderid2 = 0;
                            int.TryParse(Console.ReadLine(), out orderid2);
                            Console.WriteLine("Enter the productId of the orderitem that you want: ");
                            int prodId2 = 0;
                            int.TryParse(Console.ReadLine(), out prodId2);
                            dal.OrderItem.Update(orderid2, prodId2);
                            break;

                        case 5:
                            Console.WriteLine("Enter the orderId of the orderitem that you want to delete: ");
                            int orderid3 = 0;
                            int.TryParse(Console.ReadLine(), out orderid3);
                            Console.WriteLine("Enter the productId of the orderitem that you want to delete: ");
                            int prodId3 = 0;
                            int.TryParse(Console.ReadLine(), out prodId3);
                            dal.OrderItem.Delete(prodId3, orderid3);
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

    public static void Main(string[] args)
    {
        Display();
    }

}
