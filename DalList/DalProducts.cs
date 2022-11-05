using DO;
using System.Linq;
namespace Dal;
public class DalProducts
{
    public void AddProducts()
    {
        int ProductId = 0;
        int.TryParse(Console.ReadLine(), out ProductId);
        string ProductName = Console.ReadLine();
        double ProductPrice = 0;
        double.TryParse(Console.ReadLine(), out ProductPrice);
        Console.WriteLine("Enter the categorie 0:Men 1: women 2: children");
        string chosen = Console.ReadLine();
        DO.Category category = new DO.Category();
        category = (DO.Category)Convert.ToInt32(chosen);
        int ProductInStock = 0;
        int.TryParse(Console.ReadLine(), out ProductInStock);
        Products products=new Products();
        products.Id=ProductId;
        products.Name = ProductName;
        products.Price = ProductPrice;
        products.Category = category;
        products.InStock=ProductInStock;
        DataSource.GetAddProductToList(products);
    }
    public Products GetProduct(int ProductId)
    {
        Products products = new Products();
        foreach (var item in DataSource._products)
        {
            if(item.Id == ProductId)
                products=item;
        }
        return products;
    }
    public  Products[] GetProductList()
    {
        return DataSource._products;
    }
    public void DeletProduct(int ProductId)
    {
        int count = 0;
        foreach (var item in GetProductList())
        {
            if(item.Id == ProductId)
            {
                for (int i = count; i < DataSource.Config._productsIndex-2; i++)// Shifting elements
                {
                    GetProductList()[i]=GetProductList  ()[i+1];
                }
            }
            count++;

        }
    }
    public void UpdateProduct(int productId)
    {
        int count = 0;
        foreach (var item in GetProductList())
        {
            if (item.Id == productId)
            {
                Products NewProduct = new Products();
                NewProduct = GetProduct(productId);
                Console.WriteLine(@"choose option: 1. Update the name Product
                                           2. update the Price Product
                                           3. update the Category Product
                                           4. update the amount in the stock
                                           5. PRESS ANY KEY TO THE EXIT OPTION");
                int choice = 0;
                int.TryParse(Console.ReadLine(), out choice);
                int c = 1;
                do // the customer can update maximum three parameter after that its exit of the function
                {
                    c++;
                    if (choice == 1)// update the name
                    {
                        Console.WriteLine("Enter the new name: ");
                        string NewName = Console.ReadLine();
                        NewProduct.Name = NewName;
                    }
                    else if (choice == 2)// update the Email
                    {
                        Console.WriteLine("Enter the new Price: ");
                        int NewPrice = 0;
                        int.TryParse(Console.ReadLine(),out NewPrice);  
                        NewProduct.Price = NewPrice;
                    }
                    else if (choice == 3)// uodate the adress
                    {
                        Console.WriteLine("choose the new category 0. men 1. women 2. children");
                        int NewCategorie = 0;
                        int.TryParse(Console.ReadLine(),out NewCategorie);
                        NewProduct.Category = (Category)NewCategorie;
                    }
                    else if(choice==4)
                    {
                        Console.WriteLine("Enter the new amount in the stock: ");
                        int NewInStock = 0;
                        int.TryParse(Console.ReadLine(), out NewInStock);
                        NewProduct.InStock=NewInStock;
                    }
                    else// exit 
                    {
                        Console.WriteLine("Product updated");
                        GetProductList()[count] = NewProduct;
                        return;
                    }
                }
                while (c <= 4);
            }
            count++;
        }
        throw new Exception("Product cannot be found");

    }
}