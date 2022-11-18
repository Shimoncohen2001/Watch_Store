using DalApi;


namespace Dal;

sealed public class DalList : IDal
{
    public IProduct Product =>  new DalProducts();

    public IOrder Order =>  new DalOrder();

    public IOrderItem OrderItem => new DalOrderItems();
}
