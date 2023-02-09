using DAL;
using DalApi;
using DO;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace Dal;
internal class Product : IProduct
{
    XElement ProductRoot;
    readonly string ProductPath = @"StudentXml.xml";

    public Product()
    {
        if (!File.Exists(ProductPath))
        {
            HelpXml.CreateFiles(ProductPath);
        }
        else
        {
            HelpXml.LoadData(ProductPath);
        }
    }


    public void Add(DO.Products t)
    {
        _ = new XElement("id", t.Id);
        _ = new XElement("name", t.Name);
        _ = new XElement("price", t.Price);
        _ = new XElement("category", t.Category);
        _ = new XElement("inStock", t.InStock);

        ProductRoot.Add(new XElement("product"));
        ProductRoot.Save(ProductPath);
    }

    public void Delete(int Id1, int Id2)
    {
        XElement product;
        try
        {
            product = (from item in ProductRoot.Elements()
                       where int.Parse(item.Element("id")!.Value) == Id1
                       select item).FirstOrDefault()!;
            product.Remove();
            ProductRoot.Save(ProductPath);
        }
        catch
        {
            throw new Exception("Impossible to delete the product");
        }
    }

    public DO.Products Get(int Id1, int Id2)
    {
        HelpXml.LoadData(ProductPath);
        DO.Products? product;
        try
        {
            product = (from item in ProductRoot.Elements()
                       where int.Parse(item.Element("id")!.Value) == Id1
                       select new DO.Products()
                       {
                           Id = int.Parse(item.Element("id")!.Value),
                           Name = item.Element("name")!.Value,
                           Price = double.Parse(item.Element("price")!.Value),
                           Category = (Category)int.Parse(item.Element("category")!.Value),
                           InStock = int.Parse(item.Element("inStock")!.Value)
                       }).FirstOrDefault();
            return (DO.Products)product!;
        }
        catch
        {
            throw new Exception("Product doen't exists!");
        }
    }

    // A finir
    public DO.Products? GetItem(Func<DO.Products?, bool>? predicate)
    {
        HelpXml.LoadData(ProductPath);
        DO.Products? product;
        Predicate<Products?> predicate1 = prod => predicate!(prod);
        try
        {
            product = (from item in ProductRoot.Elements() // Voir comment utiliser le predicate
                       select new DO.Products()
                       {
                           Id = int.Parse(item.Element("id")!.Value),
                           Name = item.Element("name")!.Value,
                           Price = double.Parse(item.Element("price")!.Value),
                           Category = (Category)int.Parse(item.Element("category")!.Value),
                           InStock = int.Parse(item.Element("inStock")!.Value)
                       }).FirstOrDefault();
            return (DO.Products)product!;
        }
        catch
        {
            throw new Exception("Product doen't exists!");
        }
    }

    // Voir comment faire avec predicate
    public IEnumerable<DO.Products?> GetList(Func<DO.Products?, bool>? predicate = null)
    {
        HelpXml.LoadData(ProductPath);
        IEnumerable<DO.Products?> products;
        try
        {
            products = (IEnumerable<DO.Products?>)(from item in ProductRoot.Elements()
                        select new DO.Products()
                        {
                            Id = int.Parse(item.Element("id")!.Value),
                            Name = item.Element("name")!.Value,
                            Price = double.Parse(item.Element("price")!.Value),
                            Category = (Category)int.Parse(item.Element("category")!.Value),
                            InStock = int.Parse(item.Element("inStock")!.Value)
                        }).ToList();
            return products;
        }
        catch 
        {
            throw new Exception("Product doesn't exists!");
        }
    }

    public void Update(int Id1, int Id2)
    {
        try
        {
            XElement product = (from item in ProductRoot.Elements()
                                where int.Parse(item.Element("id")!.Value) == Id1
                                select item).FirstOrDefault()!;
            DO.Products newProduct = new()
            {
                Id = Id1
            };
            Console.WriteLine("Enter the new  name of the product you want");
            string name1 = Console.ReadLine()!;
            Console.WriteLine("Enter the new  price of the product you want");
            double price1 = 0;
            double.TryParse(Console.ReadLine(), out price1);
            Console.WriteLine("Enter the new cathegory of the product you want: 0.Men, 1. Women, 2. children");
            string choice2 = Console.ReadLine()!;
            Category category1 = (Category)Convert.ToInt32(choice2);
            Console.WriteLine("Enter the new stock of the product you want");
            int stock1 = 0;
            int.TryParse(Console.ReadLine(), out stock1);
            product.Element("name")!.Value = newProduct.Name!;
            product.Element("price")!.Value = newProduct.Price.ToString();
            product.Element("category")!.Value = newProduct.Category.ToString()!;
            product.Element("stock")!.Value = newProduct.InStock.ToString();
            ProductRoot.Save(ProductPath);
        }
        catch
        {
            throw new Exception("Product doesn't exists!");
        }
    }

    /// <summary>
    /// Saving when used with LINQ
    /// </summary>
    /// <param name="products"></param>
    public void SaveStudentList(IEnumerable<DO.Products?> products)
    {
        ProductRoot = new XElement("students",
            from item in products
            select new XElement("student",
            new XElement("id", item?.Id),
            new XElement("name", item?.Name),
            new XElement("price", item?.Price),
            new XElement("category", item?.Category),
            new XElement("inStock", item?.InStock)
            )
        );
        ProductRoot.Save(ProductPath);
    }
}