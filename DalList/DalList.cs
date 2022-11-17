using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

sealed public class DalList : IDal
{
    public IProduct Product =>  new DalProducts();

    public IOrder Order =>  new DalOrder();

    public IOrderItem OrderItem => new DalOrderItems();
}
