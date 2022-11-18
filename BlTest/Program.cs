using BlImplementation;
using BlApi;
using BO;

namespace BlTest
{
    internal class Program
    {
        public static void Display()
        {
            /*
             * Faire les try catch
             */
            IBl bl = new Bl();
            Product product = new Product();
            product.ID = 123;
            product.Name = "fbhb";
            product.Category = Category.Men;
            product.Price = 12;
            product.InStock = 1;
            bl.Product.Add(product);
        }

        static void Main(string[] args)
        {
            Display();
        }
    }
}