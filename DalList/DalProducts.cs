using DO;
using System.Linq;
namespace Dal;
public class DalProducts
{

    /// <summary>
    /// ///////////////////////////Add//////////////////////////////
    /// </summary>
    /// <param name="products"></param>
    public void AddProducts(Products products)
    {
        DataSource.GetAddProductToList(products);
    }

    /// <summary>
    /// ///////////////////////////GetById///////////////////////////
    /// </summary>
    /// <param name="ProductId"></param>
    /// <returns></returns>
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

    /// <summary>
    /// ////////////////////////////GetList////////////////////////
    /// </summary>
    /// <returns></returns>
    public  Products[] GetProductList()
    {
        return DataSource._products;
    }

    /// <summary>
    /// //////////////////////////Delete///////////////////////////
    /// </summary>
    /// <param name="ProductId"></param>
    public void DeletProduct(int ProductId)
    {
        int count = 0;
        foreach (var item in GetProductList())
        {
            if(item.Id == ProductId)
            {
                for (int i = count; i < DataSource.Config._productsIndex; i++)// Shifting elements
                {
                    GetProductList()[i]= GetProductList()[i+1];
                    
                }
            }
            count++;

        }
    }

    /// <summary>
    /// //////////////////////////Update/////////////////////////
    /// </summary>
    /// <param name="productId"></param>
    /// <exception cref="Exception"></exception>
    public void UpdateProduct(int productId)
    {
        int count = 0;
        foreach (var item in GetProductList())
        {
            if (item.Id == productId)
            {
                Products NewProduct = new Products();
                NewProduct = GetProduct(productId);
                bool finish = false;
                while(!finish)
                {

                Console.WriteLine(@"choose option: 
                                           1. Update the name Product
                                           2. update the Price Product
                                           3. update the Category Product
                                           4. update the amount in the stock
                                           5. EXIT ");
                int choice = 0;
                int.TryParse(Console.ReadLine(), out choice);
                    if (choice == 1)// update the name
                    {
                        Console.WriteLine("Enter the new Name: ");
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
                        Console.WriteLine("Choose the new category 0. Men 1. Women 2. Children");
                        int NewCategorie = 0;
                        int.TryParse(Console.ReadLine(),out NewCategorie);
                        NewProduct.Category = (Category)NewCategorie;
                    }
                    else if(choice == 4)
                    {
                        Console.WriteLine("Enter the new Amount in the stock: ");
                        int NewInStock = 0;
                        int.TryParse(Console.ReadLine(), out NewInStock);
                        NewProduct.InStock=NewInStock;
                    }
                    else// exit 
                    {
                        Console.WriteLine("Product updated.");
                        DataSource._products[count] = NewProduct;
                        return;
                    }
                }
            }
            count++;
        }
        throw new Exception("Product cannot be found!");

    }
}