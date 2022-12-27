using DO;
using DalApi;
using System;
using System.Reflection;

namespace Dal;
internal class DalProducts : IProduct
{

    /// <summary>
    /// ///////////////////////////Add//////////////////////////////
    /// </summary>
    /// <param name="products"></param>
    public void Add(Products products)
    {
        DataSource.GetAddProductToList(products);
    }

    /// <summary>
    /// ///////////////////////////GetById///////////////////////////
    /// </summary>
    /// <param name="ProductId"></param>
    /// <returns></returns>
    public Products Get(int ProductId,int v=0)
    {
        Products products = new Products();
        var products1 = from item in DataSource._products
                        where item?.Id == ProductId
                        select item;


        products = (Products)products1.First()!; // products1 is an enumerable that contains just one product
        return products!;
    }

    /// <summary>
    /// ////////////////////////////GetList////////////////////////
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Products?> GetList(Func<Products?, bool>? predicate=null)
    {
        if (predicate!=null)
        {
            //Predicate<Products?> predicate1=(Prod)=> predicate(Prod);
            var newList = DataSource._products.Where(predicate); // newList selects just the products that who pass the condition
            return newList;
        }
        return DataSource._products;
    }

    /// <summary>
    /// //////////////////////////Delete///////////////////////////
    /// </summary>
    /// <param name="ProductId"></param>
    public void Delete(int ProductId, int v = 0)
    {
        DataSource._products.Remove(Get(ProductId));
    }

    /// <summary>
    /// //////////////////////////Update/////////////////////////
    /// </summary>
    /// <param name="productId"></param>
    /// <exception cref="Exception"></exception>
    public void Update(int productId, int v = 0)
    {
        int count = 0;
        var products = from item in DataSource._products
                       where item?.Id == productId
                       select item;  // products is an enumarable that contains the old product and the updated product
        foreach (var item in products)
        {
            //if (item?.Id == productId)
            //{
            //Products NewProduct = new Products();
            //NewProduct = (Products)Get(productId);
            //bool finish = false;
            //while (!finish)
            //{

            //    Console.WriteLine(@"choose option: 
            //                           1. Update the name Product
            //                           2. update the Price Product
            //                           3. update the Category Product
            //                           4. update the amount in the stock
            //                           5. EXIT ");
            //    int choice = 0;
            //    int.TryParse(Console.ReadLine(), out choice);
            //    if (choice == 1)// update the name
            //    {
            //        Console.WriteLine("Enter the new Name: ");
            //        string NewName = Console.ReadLine();
            //        NewProduct.Name = NewName;
            //    }
            //    else if (choice == 2)// update the Email
            //    {
            //        Console.WriteLine("Enter the new Price: ");
            //        int NewPrice = 0;
            //        int.TryParse(Console.ReadLine(), out NewPrice);
            //        NewProduct.Price = NewPrice;
            //    }
            //    else if (choice == 3)// uodate the adress
            //    {
            //        Console.WriteLine("Choose the new category 0. Men 1. Women 2. Children");
            //        int NewCategorie = 0;
            //        int.TryParse(Console.ReadLine(), out NewCategorie);
            //        NewProduct.Category = (Category)NewCategorie;
            //    }
            //    else if (choice == 4)
            //    {
            //        Console.WriteLine("Enter the new Amount in the stock: ");
            //        int NewInStock = 0;
            //        int.TryParse(Console.ReadLine(), out NewInStock);
            //        NewProduct.InStock = NewInStock;
            //    }
            //    else// exit 
            //    {
            //Console.WriteLine("Product updated.");
                //    }
                //}
            //}
            //count++;

            count = DataSource._products.FindIndex(p => p.Equals(item));
            DataSource._products[count] = DataSource._products.Last();
            DataSource._products.RemoveAt(DataSource._products.Count()-1);
            return;
        }
        throw new Exception("Product cannot be found!");

    }

    /// <summary>
    /// Return a specific product
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public Products? GetItem(Func<Products?, bool>? predicate)
    {
        Predicate<Products?> predicate1 = prod => predicate!(prod);
        Products? p = DataSource._products.Find(predicate1);
        return p;
    }
}