using BlApi;

using Dal;
using DalApi;

namespace BlImplementation;

internal class BlProduct : BlApi.IProduct 
{
    private IDal Dal = new DalList();  // Permet d'acceder au mofa de l'interface Products de la DAL
    
    public void Add(BO.Product product)
    {
        /*
         * Faire les if des harigot ici direct comme ça pas besoin d'effectuer la fonction si la logique est pas respectée
         */
        DO.Products products = new DO.Products();
        products.Id = product.ID;
        products.Name = product.Name;
        products.Price = product.Price;
        products.Category = (DO.Category)product.Category;
        products.InStock = product.InStock;
        Dal.Product.Add(products);
    }

    
}
