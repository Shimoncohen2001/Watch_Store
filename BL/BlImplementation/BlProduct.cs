using Dal;
using DalApi;

namespace BlImplementation;
internal class BlProduct : BlApi.IProduct
{
    private IDal Dal = new DalList();
    /// <summary>
    /// Copy the list of Products from DataSource to a list of BO.ProductForList and return the list
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.ProductForList?> GetProductForLists()
    {
        List<BO.ProductForList?> productsForList = new List<BO.ProductForList?>();
        foreach (var item in Dal.Product.GetList())
        {
            BO.ProductForList p = new BO.ProductForList()
            {
                ID = (int)item?.Id,
                Name = (string)item?.Name,
                Price = (double)item?.Price,
                Category = (BO.Category)item?.Category
            };
            productsForList.Add(p);
        }
        return productsForList;
    }

    /// <summary>
    /// Check if the ID of the Product exist and is valid for to return the Product or not for the director
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    public BO.Product GetDirector(int productId)
    {
        if (productId < 0)
            throw new BO.IdNotValidExcpetion(" Impossible negative Id! ");
        List<DO.Products?> productsForLists = (List<DO.Products?>)Dal.Product.GetList();
        foreach (var item in productsForLists)
        {
            if (item?.Id == productId)
            {
                DO.Products products = Dal.Product.Get(productId, 0);
                BO.Product product = new BO.Product()
                {
                    ID = products.Id,
                    Name = products.Name,
                    Price = products.Price,
                    Category = (BO.Category)products.Category,
                    InStock = products.InStock
                };
                return product;
            }
        }
        throw new BO.NoExistingItemException("Product doesn't exist!"); // Positive ID but not exist in the list of Products
    }

    /// <summary>
    /// Check if the ID of the Product exist and is valid for to return the Product or not for the client
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    public BO.ProductItem GetClient(int productId, BO.Cart cart)
    {
        if (productId < 0)
            throw new BO.IdNotValidExcpetion("Invalid ID!");
        List<DO.Products?> productsForLists = (List<DO.Products?>)Dal.Product.GetList();
        foreach (var item in productsForLists)
        {
            if (item?.Id == productId)
            {
                DO.Products products = Dal.Product.Get(productId, 0);
                int index = cart.orderItems.FindIndex(OrderItems => OrderItems.ProductID == productId);
                BO.ProductItem productItem = new BO.ProductItem()
                {
                    ID = productId,
                    Name = products.Name,
                    Price = products.Price,
                    Category = (BO.Category)products.Category,
                    Amount = cart.orderItems[index].Amount,
                };
                if (products.InStock > 0)
                    productItem.InStock = true;
                else
                    productItem.InStock = false;
                return productItem;
            }
        }
        throw new BO.NoExistingItemException("Product doesn't exist!"); // Positive ID but not exist in the list of Products
    }

    /// <summary>
    /// Test if the data of the product is right for to ad him in the list of Products in the DataSource or not
    /// </summary>
    /// <param name="product"></param>
    public void Add(BO.Product product)
    {
        if (product.ID < 0)
            throw new BO.IdNotValidExcpetion("Invalid ID!");
        if (product.Name.Length == 0)
            throw new BO.NameNotValidExcpetion("Invalid name!");
        if (product.Price < 0)
            throw new BO.PriceNotValidExcpetion("Invalid Price!");
        if (product.InStock <= 0)
            throw new BO.StockNotValidExcpetion("Invalid stock!");
        if (Dal.Product.GetList().ToList().Exists(Product => Product?.Id == product.ID))
            throw new BO.ItemAlreadyExistException("this product already exist");
        DO.Products products = new DO.Products()
        {
            Id = product.ID,
            Name = product.Name,
            Price = product.Price,
            Category = (DO.Category)product.Category,
            InStock = product.InStock
        };
        Dal.Product.Add(products);
    }

    /// <summary>
    /// Delete a Product from the list of Products in the DataSource if its possible
    /// </summary>
    /// <param name="id"></param>
    public void Delete(int productId)
    {
        
        if (Dal.OrderItem.GetList().ToList().Exists(OrderItem=> OrderItem?.ProductId==productId))
        {
            throw new BO.DeleteItemNotValidExcpetion("Impossible to delete product!");
        }
        if (!Dal.Product.GetList().ToList().Exists(Product => Product?.Id == productId))
        {
            throw new BO.NoExistingItemException("Product mot exist");
        }
        List<BO.ProductForList?> listOfProduct = (List<BO.ProductForList?>)GetProductForLists();
        foreach (var item in listOfProduct)
        {
            if (item?.ID == productId)
            {
                DO.Products products = new DO.Products();
                products.Id = item.ID;
                Dal.Product.Delete(productId, 0);
            }
        }

    }

    /// <summary>
    /// Update directly the product in the DataSource
    /// </summary>
    /// <param name="productId"></param>
    public void Update(BO.Product product)
    {
        if (product.ID < 0)
            throw new BO.IdNotValidExcpetion("Invalid ID!");
        if (product.Name.Length == 0)
            throw new BO.NameNotValidExcpetion("Invalid name!");
        if (product.Price < 0)
            throw new BO.PriceNotValidExcpetion("Invalid Price!");
        if (product.InStock < 0)
            throw new BO.StockNotValidExcpetion("Invalid stock!");
        List<DO.Products?> productsForLists = (List<DO.Products?>)Dal.Product.GetList();
        foreach (var item in productsForLists)
        {
            if (item?.Id == product.ID)
            {
                if (product.Name != item?.Name)
                    throw new BO.NameNotValidExcpetion("Invalid name!");
                if (product.Price != item?.Price)
                    throw new BO.PriceNotValidExcpetion("Invalid price!");
                if (product.InStock != item?.InStock)
                    throw new BO.StockNotValidExcpetion("Invalid stock!");
                DO.Products products = new DO.Products()
                {
                    Id = product.ID,
                    Name = product.Name,
                    Price = product.Price,
                    Category = (DO.Category)product.Category,
                    InStock = product.InStock
                };
                Dal.Product.Update(products.Id, 0);
                break;
            }
        }
    }
}