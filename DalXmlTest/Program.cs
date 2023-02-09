using DO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;

static class Program
{
    public static void Main(string[] args)
    {
        //DalApi.IDal? dal = DalApi.Factory.Get(); // genere exception dans factory.cs
        //Products products = new();
        //for (int i = 1; i <= 10; i++)
        //{
        //    Products products1 = new Products { Id = i, Name = "first" + i, Price = i * 2, Category = Category.Men, InStock = i };
        //    dal?.Product.Add(products1);

        //}

        //product.Delete(5, 0);
        //product.Update(2, 0);
        //IEnumerable<Products?> products2 = product.GetList()!;

        //foreach (var item in products2)
        //{
        //    Console.WriteLine("id:{0, -5} name:{1} Price:{2} Category:{3} InStock:{4]", item?.Id, item?.Name, item?.Price, item?.Category, item?.InStock);
        //}
    }
}

