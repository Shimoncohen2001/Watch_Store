using DalApi;
using System.Runtime.CompilerServices;

namespace BlImplementation;

internal class BlOrder : BlApi.IOrder
{
    DalApi.IDal? dal = DalApi.Factory.Get();  
    // Permet d'acceder au mofa de l'interface Order de la DAL
      //public IEnumerable<BO.OrderForList?> GetOrderList(Func<BO.OrderForList?, bool>? func = null)// for the admin only
      //{
      //    int amountOfItems = 0;
      //    double totalPrice = 0;
      //    int status = 0;
      //    List<BO.OrderForList?> listOrders = new List<BO.OrderForList?>();
      //    foreach (var item in dal?.Order.GetList())
      //    {
      //        amountOfItems = 0;
      //        totalPrice = 0;
      //        var Amount = from tot in dal?.OrderItem.GetList()// we create a list that contain only the amount of items of a specific order by his id
      //                     where tot?.OrderId == item?.Id
      //                     select tot?.Amount;
      //        amountOfItems = (int)Amount.Sum();// we calculate the sum of our previous list of Amount
      //        totalPrice = GetOrderItem((int)item?.Id!).TotalPrice;// we use the GetOrderItem to get the Total price of this order
      //        if (item?.OrderDate <= DateTime.Now && (item?.ShipDate > DateTime.Now || item?.ShipDate == DateTime.MinValue))
      //        {
      //            status = 0;
      //        }
      //        else if ((item?.ShipDate <= DateTime.Now && item?.DeliveryDate > DateTime.Now) || (item?.ShipDate != DateTime.MinValue && item?.DeliveryDate == DateTime.MinValue))
      //        {
      //            status = 1;
      //        }
      //        else if (item?.DeliveryDate <= DateTime.Now && item?.DeliveryDate != DateTime.MinValue)
      //        {
      //            status = 2;
      //        }
      //        BO.OrderForList orderForList = new BO.OrderForList()
      //        {
      //            CustomerName = item?.CustomerName,
      //            ID = (int)item?.Id!,
      //            AmountOfItems = amountOfItems,
      //            TotalPrice = totalPrice,
      //            Status = (BO.OrderStatus)status
      //        };
      //        listOrders.Add(orderForList);
      //    }
      //    if (func != null)
      //    {
      //        Predicate<BO.OrderForList?> predicate1 = (ord) => func(ord);
      //        var newList = listOrders.FindAll(predicate1);
      //        return newList;
      //    }
      //    return listOrders;
      //}

    [MethodImpl(MethodImplOptions.Synchronized)]

    public IEnumerable<BO.OrderForList> GetOrderList(Func<BO.OrderForList, bool> func = null)
    {
        lock (dal!)
        {
            List<BO.OrderForList> listOrders = new List<BO.OrderForList>();

            var orderItems = dal.OrderItem.GetList();
            var orders = dal.Order.GetList().Where(o=>orderItems.Any(i=>o?.Id==i?.OrderId));// select only the orders that they have an orderitems

            var query = from order in orders
                        let orderItemsForOrder = orderItems.Where(oi => oi?.OrderId == order?.Id)// we put on this list all the orderItems that verify the predicate in the where function
                        let amountOfItems = orderItemsForOrder.Sum(oi => oi?.Amount)// we calculate the sum of amountOfItems of the previous list
                        let totalPrice = GetOrderItem((int)order?.Id).TotalPrice// we calculate the totalPrice of all the items in the order
                        select new
                        {
                            Order = order,
                            AmountOfItems = amountOfItems,
                            TotalPrice = totalPrice
                        };

            foreach (var item in query)
            {
                int status=0;
                if (item.Order?.OrderDate <= DateTime.Now && (item.Order?.ShipDate > DateTime.Now || item.Order?.ShipDate == null))
                {
                    status = 0;
                }
                else if ((item.Order?.ShipDate <= DateTime.Now && item.Order?.DeliveryDate > DateTime.Now) || (item.Order?.ShipDate != DateTime.MinValue && (item.Order?.DeliveryDate == null)))
                {
                    status = 1;
                }
                else if (item.Order?.DeliveryDate <= DateTime.Now && item.Order?.DeliveryDate != null)
                {
                    status = 2;
                }
                

                BO.OrderForList orderForList = new BO.OrderForList()
                {
                    CustomerName = item.Order?.CustomerName,
                    ID = (int)item.Order?.Id,
                    AmountOfItems = (int)item?.AmountOfItems,
                    TotalPrice = item.TotalPrice,
                    Status = (BO.OrderStatus)status
                };
                listOrders.Add(orderForList);
            }

            if (func != null)// if there is any request for  displaying the list
            {
                Predicate<BO.OrderForList> predicate = (ord) => func(ord);
                var newList = listOrders.FindAll(predicate);
                return newList;
            }

            return listOrders;

        }
    }

    //public BO.Order GetOrderItem(int OrderId)
    //{
    //    double totalPrice = 0;
    //    List<BO.OrderItem?> orderItemsList = new List<BO.OrderItem?>();
    //    if (OrderId > 0)
    //    {
    //        foreach (var item in dal?.Order.GetList())
    //        {
    //            int OrderitemId = 0;
    //            if (item?.Id == OrderId)
    //            {
    //                foreach (var item1 in dal?.OrderItem.GetList())
    //                {
    //                    OrderitemId++;
    //                    if (item1?.OrderId == OrderId)
    //                    {
    //                        int ProductId = (int)item1?.ProductId!;
    //                        string productName = dal?.Product.Get((int)item1?.ProductId!, 0).Name!;
    //                        double price = (double)dal?.Product.Get((int)item1?.ProductId!, 0).Price;
    //                        int Amount = Convert.ToInt32((int)item1?.Amount!);
    //                        BO.OrderItem orderItem = new BO.OrderItem()
    //                        {
    //                            Name = productName,
    //                            Price = price,
    //                            Amount = Amount,
    //                            ProductID = ProductId,
    //                            ID = OrderitemId,
    //                            TotalPrice = Amount * price
    //                        };
    //                        orderItemsList.Add(orderItem);
    //                        totalPrice += orderItem.TotalPrice;
    //                    }//tout ceci marche uniquemetn dans le cas ou tout estv rempli comme il se doit 
    //                    // c'est a dire qu'il ne se peut pas que deux orderitems ayant le meme orderId possede egalemt le meme product id
    //                    // donc gerer bien ca
    //                }
    //                BO.Order order = new BO.Order()
    //                {
    //                    Id = OrderId,
    //                    CustomerAdress = item?.CustomerAdress,
    //                    CustomerEmail = item?.CustomerEmail,
    //                    CustomerName = item?.CustomerName,
    //                    OrderDate = item?.OrderDate!,
    //                    ShipDate = item?.ShipDate! ?? DateTime.MinValue,
    //                    DeliveryDate = item?.DeliveryDate! ?? DateTime.MinValue,
    //                    PaymentDate = (DateTime)item?.OrderDate!,
    //                    TotalPrice = totalPrice,
    //                    orderItems = orderItemsList
    //                };
    //                if (order.ShipDate > DateTime.Now || order.ShipDate == DateTime.MinValue)
    //                {
    //                    order.Status = BO.OrderStatus.Approved;
    //                }
    //                else if (order.ShipDate < DateTime.Now && (order.DeliveryDate > DateTime.Now || order.DeliveryDate == DateTime.MinValue))
    //                {
    //                    order.Status = BO.OrderStatus.Expedited;
    //                }
    //                else if (order.DeliveryDate <= DateTime.Now && order.DeliveryDate != DateTime.MinValue)
    //                {
    //                    order.Status = BO.OrderStatus.Received;
    //                }
    //                return order;
    //            }
    //        }
    //        throw new BO.NoExistingItemException("Order not exist");
    //    }
    //    else
    //    {
    //        throw new BO.IdNotValidExcpetion("Invalid Id!");
    //    }
    //}

    [MethodImpl(MethodImplOptions.Synchronized)]

    public BO.Order GetOrderItem(int OrderId)
    {

        lock (dal!)
        {

            if (OrderId <= 0)
            {
                throw new BO.IdNotValidExcpetion("Invalid Id!");
            }

            var orderItems = dal?.OrderItem.GetList();
            var orders = dal?.Order.GetList().Where(o => orderItems!.Any(i => o?.Id == i?.OrderId));// select only the orders that they have an orderItem
            var products = dal?.Product.GetList();

            var query = from order1 in orders
                        join orderItem in orderItems! on order1?.Id equals orderItem?.OrderId
                        join product in products! on orderItem?.ProductId equals product?.Id
                        where order1?.Id == OrderId
                        select new
                        {
                            Order = order1,
                            OrderItem = orderItem,
                            Product = product
                        };

            if (!query.Any())
            {
                throw new BO.NoExistingItemException("Order not exist");
            }

            BO.Order order = new BO.Order()
            {
                Id = OrderId,
                CustomerAdress = query.First().Order?.CustomerAdress,
                CustomerEmail = query.First().Order?.CustomerEmail,
                CustomerName = query.First().Order?.CustomerName,
                OrderDate = query.First().Order?.OrderDate,
                ShipDate = query.First().Order?.ShipDate ?? DateTime.MinValue,
                DeliveryDate = query.First().Order?.DeliveryDate ?? DateTime.MinValue,
                PaymentDate = query.First().Order?.OrderDate,
                TotalPrice = query.Sum(item => (int)item.OrderItem?.Amount * (double)item.Product?.Price),
                orderItems = query.Select(item => new BO.OrderItem()
                {
                    Name = item.Product?.Name,
                    Price = (double)item.Product?.Price,
                    Amount = (int)item.OrderItem?.Amount,
                    ProductID = (int)item.Product?.Id,
                    ID = (int)item.OrderItem?.Id,
                    TotalPrice = (int)item.OrderItem?.Amount * (double)item.Product?.Price
                }).ToList()!
            };

            if (order.ShipDate > DateTime.Now || order.ShipDate == DateTime.MinValue)
            {
                order.Status = BO.OrderStatus.Approved;
            }
            else if (order.ShipDate < DateTime.Now && (order.DeliveryDate > DateTime.Now || order.DeliveryDate == DateTime.MinValue))
            {
                order.Status = BO.OrderStatus.Expedited;
            }
            else if (order.DeliveryDate <= DateTime.Now && order.DeliveryDate != DateTime.MinValue)
            {
                order.Status = BO.OrderStatus.Received;
            }

            return order;
        }

    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    public BO.Order UpdateOrderShipping(int OrderId)
    {

        lock (dal!)
        {
            if ((bool)dal?.Order.GetList().ToList().Exists(Order => Order?.Id == OrderId&&(Order?.ShipDate > DateTime.Now || Order?.ShipDate == DateTime.MinValue || Order?.ShipDate==null)))// test if the Order With the specific OrderiD EXIST and already not sent
            {
                BO.Order order1= new BO.Order();
                order1 =GetOrderItem(OrderId); 
                order1.ShipDate=DateTime.Now;
                order1.Status = BO.OrderStatus.Expedited;
                DO.Order order = new DO.Order() {
                    Id = (int)dal?.Order.Get(OrderId, 0).Id,
                    CustomerAdress = dal?.Order.Get(OrderId, 0).CustomerAdress,
                    CustomerEmail = dal?.Order.Get(OrderId, 0).CustomerEmail,
                    CustomerName = dal?.Order.Get(OrderId, 0).CustomerName,
                    DeliveryDate = dal?.Order.Get(OrderId, 0).DeliveryDate,
                    OrderDate = dal?.Order.Get(OrderId, 0).OrderDate,
                    ShipDate = DateTime.Now
                };
                dal?.Order.Delete(OrderId, 0);//remove on the dal
                dal?.Order.Add(order);// Add the updated Order to the dal
                return order1;
            }
            else if ((bool)dal?.Order.GetList().ToList().Exists(Order => Order?.Id == OrderId && (!(Order?.ShipDate > DateTime.Now) || !(Order?.ShipDate == DateTime.MinValue))))
            {
                throw new BO.OrderAlreadyExpeditedException("Order already expedited");
            }
            else
            {
                throw new BO.NoExistingItemException("Order Not exist");
            }
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]

    public BO.Order UpdadteOrderReceived(int OrderId)
    {

        lock (this)
        {
            if ((bool)dal?.Order.GetList().ToList().Exists(Order => Order?.Id == OrderId && (Order?.DeliveryDate > DateTime.Now || Order?.DeliveryDate == DateTime.MinValue||Order?.DeliveryDate==null)))//  test if the Order With the specific OrderiD EXIST and already not Received
            {
                BO.Order order = new BO.Order();
                order=GetOrderItem(OrderId);
                order.DeliveryDate=DateTime.Now;
                order.Status = BO.OrderStatus.Received;
                DO.Order order1 = new DO.Order()
                {
                    Id = order.Id,
                    OrderDate = order.DeliveryDate,
                    DeliveryDate = order.DeliveryDate,
                    ShipDate = order.ShipDate,
                    CustomerAdress = order.CustomerAdress,
                    CustomerEmail = order.CustomerEmail,
                    CustomerName= order.CustomerName
                };

                //var orderitems = dal.OrderItem.GetList().ToList().Where(o => o?.OrderId == OrderId);
                //foreach (var item in orderitems)
                //{
                //    dal?.OrderItem.Delete((int)item?.ProductId!, (int)item?.OrderId!);
                //}

                dal?.Order.Delete(OrderId,0);// the Order was delivered to the client so we nead to remove it from the orderList
                dal?.Order.Add(order1);
                return order;
            }
            else if((bool)dal?.Order.GetList().ToList().Exists(Order => Order?.Id == OrderId && !(Order?.DeliveryDate > DateTime.Now || Order?.DeliveryDate == DateTime.MinValue)))
            {
                throw new BO.OrderAlreadyReceivedException("Order already received!");
            }
            else
            {
                throw new BO.NoExistingItemException("Order not exist");
            }
            // faire aussi try catch

        }
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    public BO.OrderTracking TrackingOrder(int OrderId)
    {
        lock (dal!)
        {
            if ((bool)dal?.Order.GetList().ToList().Exists(Order => Order?.Id == OrderId))// test if the order Exist on the list of order of the dal
            {
                BO.OrderTracking orderTracking = new BO.OrderTracking();
                orderTracking.ID = OrderId;
                int index = GetOrderList().ToList().FindIndex(Order => Order!.ID == OrderId);
                orderTracking.Status = GetOrderList().ToList()[index]!.Status;// get the status value of the list of order of the bl
                Tuple<DateTime?, string?>? description1 = new (GetOrderItem(OrderId).PaymentDate, "Order Appoved");
                Tuple<DateTime?, string?>? description2 = new (GetOrderItem(OrderId).ShipDate, "Order Expedied");
                Tuple<DateTime?, string?>? description3 = new (GetOrderItem(OrderId).DeliveryDate, "Order Received");
                orderTracking.OrderTrackingList!.Add(description1);
                orderTracking.OrderTrackingList.Add(description2);
                orderTracking.OrderTrackingList.Add(description3);
                return orderTracking;
            }
            else
            {
                throw new BO.NoExistingItemException("Order not exist");
            }

        }
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int GetOldestOrder()
    {
        lock (dal!)
        {
            BO.Order? order = null;
            //return only the relevant orders order according the dates
            var orders = (from item in dal?.Order.GetList()
                          where item?.DeliveryDate == null
                          orderby item?.ShipDate ?? item?.OrderDate
                          select item).ToList();
            if (orders.Count == 0)
                return order.Id;
            return (int) orders.FirstOrDefault()?.Id!;

        }
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void DeletOrderForAdmin(int OrderId)
    {
        lock (dal!)
        {
            if (dal?.Order.Get(OrderId,0).DeliveryDate!=null || dal?.Order.Get(OrderId, 0).DeliveryDate != DateTime.MinValue)
            {
                 var orderitems = dal.OrderItem.GetList().ToList().Where(o => o?.OrderId == OrderId);
                 foreach (var item in orderitems)
                 {
                     dal?.OrderItem.Delete((int)item?.ProductId!, (int)item?.OrderId!);
                 }
                 dal?.Order.Delete(OrderId, 0);
            }
            else
            {
                throw new Exception("Impossible to delete Order");
            }
        }
    }
}
