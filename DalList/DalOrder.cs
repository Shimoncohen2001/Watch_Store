using DO;
using DalApi;
using System.Dynamic;
using System;

namespace Dal;
internal class DalOrder:IOrder
{
    /// <summary>
    /// //////////////////////////Add/////////////////////////
    /// </summary>
    /// <param name="order"></param>
    public void Add(Order order) 
    {
        DataSource.GetAddOrderToList(order);
    }

    /// <summary>
    /// //////////////////////////GetById////////////////////
    /// </summary>
    /// <param name="OrderId"></param>
    /// <returns></returns>
    public Order Get(int OrderId, int val=0)
    {

        //foreach (var item in order1)
        //{
        //    order = item;
        //}
        Order order = new Order();
        var order1 = from item in DataSource._orders
                     where item?.Id == OrderId
                     select item;
        order = (Order)order1.First()!; //order1 is an enumerable that contains just one order
        return order;
    }

    /// <summary>
    /// //////////////////////GetList/////////////////////////
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Order?> GetList(Func<Order?,bool> func = null!)
    {
        if (func != null)
        {
            var newList = DataSource._orders.Where(func); // newList selects just the orders who pass the condition
            return newList;
        }
        return DataSource._orders;
    }

    /// <summary>
    /// /////////////////////Delete///////////////////////////
    /// </summary>
    /// <param name="orderId"></param>
    public void Delete(int orderId,int v=0)
    {
        DataSource._orders.Remove(Get(orderId));
        //DataSource.Config._automaticOrderId--;
    }

    /// <summary>
    /// //////////////////////////Update///////////////////////////////////
    /// </summary>
    /// <param name="orderId"></param>
    public void Update(int orderId,int v=0)
    {
        int count = 0;
        var newOrder = from item in DataSource._orders
                       where item?.Id == orderId
                       select item;
        count = DataSource._orders.FindIndex(o => o?.CustomerName == newOrder.First()?.CustomerName && o?.CustomerEmail == newOrder.First()?.CustomerEmail && o?.CustomerAdress == newOrder.First()?.CustomerAdress);
        DataSource._orders[count] = DataSource._orders.Last();
        DataSource._orders.RemoveAt(DataSource._orders.Count() - 1);
        return;
        throw new Exception("Order cannot be found!");
    }

    public Order? GetItem(Func<Order?, bool>? predicate)
    {
        Predicate<Order?> predicate1 = ord => predicate!(ord);
        Order? o = DataSource._orders.Find(predicate1);
        return o;
    }
}
