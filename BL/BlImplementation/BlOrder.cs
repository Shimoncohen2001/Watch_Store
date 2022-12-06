using Dal;
using DalApi;

namespace BlImplementation;

internal class BlOrder : BlApi.IOrder
{
    private IDal Dal = new DalList();  // Permet d'acceder au mofa de l'interface Order de la DAL
    public IEnumerable<BO.OrderForList> GetOrderList()// for the admin only
    {
        int amountOfItems = 0;
        double totalPrice=0;
        int status=0;
        List<BO.OrderForList> listOrders = new List<BO.OrderForList>();
        foreach (var item in Dal.Order.GetList())
        {
            amountOfItems = 0;
            totalPrice=0;
            foreach (var item1 in Dal.OrderItem.GetList())
            {
                if (item1.OrderId==item.Id)
                {
                    amountOfItems+=item1.Amount;
                    //totalPrice+=amountOfItems*(item1.Price);
                    totalPrice = GetOrderItem(item1.OrderId).TotalPrice;
                }
            }
            if (item.OrderDate<=DateTime.Now && (item.ShipDate>DateTime.Now|| item.ShipDate==DateTime.MinValue))
            {
                 status = 0;
            }
            else if((item.ShipDate<=DateTime.Now && item.DeliveryDate>DateTime.Now)||(item.ShipDate!=DateTime.MinValue && item.DeliveryDate==DateTime.MinValue ) )
            {
                status = 1;
            }
            else if (item.DeliveryDate<=DateTime.Now&& item.DeliveryDate!=DateTime.MinValue)
            {
                status = 2;
            }
                BO.OrderForList orderForList = new BO.OrderForList() { CustomerName=item.CustomerName,
                ID=item.Id, 
                AmountOfItems=amountOfItems, 
                TotalPrice=totalPrice, 
                Status=(BO.OrderStatus)status};
                listOrders.Add(orderForList);
        }
        return listOrders;
    }
    public BO.Order GetOrderItem(int OrderId)
    {
        double totalPrice = 0; 
        List<BO.OrderItem> orderItemsList = new List<BO.OrderItem>();
        if (OrderId>0)
        {
            foreach (var item in Dal.Order.GetList())
            {
                int OrderitemId = 0;
                if (item.Id==OrderId)
                {
                    foreach (var item1 in Dal.OrderItem.GetList())
                    {
                        OrderitemId++;
                        if(item1.OrderId==OrderId)
                        {
                            int ProductId=item1.ProductId;
                            string productName=Dal.Product.Get(item1.ProductId,0).Name;
                            double price= Dal.Product.Get(item1.ProductId, 0).Price;
                            int Amount=Convert.ToInt32(item1.Amount);
                            BO.OrderItem orderItem=new BO.OrderItem() { Name=productName,
                                Price=price,
                                Amount=Amount,
                                ProductID=ProductId,
                                ID=OrderitemId,
                                TotalPrice=Amount*price
                                };
                            orderItemsList.Add(orderItem);
                            totalPrice += orderItem.TotalPrice;
                        }//tout ceci marche uniquemetn dans le cas ou tout estv rempli comme il se doit 
                        // c'est a dire qu'il ne se peut pas que deux orderitems ayant le meme orderId possede egalemt le meme product id
                        // donc gerer bien ca
                    }
                    BO.Order order = new BO.Order()
                    {
                        Id = OrderId,
                        CustomerAdress = item.CustomerAdress,
                        CustomerEmail = item.CustomerEmail,
                        CustomerName = item.CustomerName,
                        DeliveryDate = item.DeliveryDate,
                        OrderDate = item.OrderDate,
                        ShipDate = item.ShipDate,
                        PaymentDate = item.OrderDate,
                        TotalPrice = totalPrice,
                        orderItems= orderItemsList
                    };
                    if(order.ShipDate>DateTime.Now || order.ShipDate==DateTime.MinValue)
                    {
                        order.Status=BO.OrderStatus.Approved;
                    }
                    else if (order.ShipDate<DateTime.Now && (order.DeliveryDate>DateTime.Now || order.DeliveryDate==DateTime.MinValue))
                    {
                        order.Status = BO.OrderStatus.Expedited;
                    }
                    else if (order.DeliveryDate<=DateTime.Now && order.DeliveryDate!=DateTime.MinValue)
                    {
                        order.Status = BO.OrderStatus.Received;
                    }
                    return order;
                }
            }
            throw new BO.NoExistingItemException("Order not exist");
        }
        else
        {
            throw new BO.IdNotValidExcpetion("Invalid Id!");
        }
    }
    
    public BO.Order UpdateOrderShipping(int OrderId)
    {
        if (Dal.Order.GetList().ToList().Exists(Order => Order.Id == OrderId&&(Order.ShipDate>DateTime.Now||Order.ShipDate==DateTime.MinValue)))// test if the Order With the specific OrderiD EXIST and already not sent
        {
            BO.Order order1= new BO.Order();
            order1=GetOrderItem(OrderId);
            order1.ShipDate=DateTime.Now;
            order1.Status = BO.OrderStatus.Expedited;
            DO.Order order = new DO.Order() {
                Id = Dal.Order.Get(OrderId, 0).Id,
                CustomerAdress = Dal.Order.Get(OrderId, 0).CustomerAdress,
                CustomerEmail = Dal.Order.Get(OrderId, 0).CustomerEmail,
                CustomerName = Dal.Order.Get(OrderId, 0).CustomerName,
                DeliveryDate = Dal.Order.Get(OrderId, 0).DeliveryDate,
                OrderDate = Dal.Order.Get(OrderId, 0).OrderDate,
                ShipDate = DateTime.Now
            };
            Dal.Order.Delete(OrderId, 0);//remove on the dal
            Dal.Order.Add(order);// Add the updated Order to the dal
            return order1;
        }
        else if (Dal.Order.GetList().ToList().Exists(Order => Order.Id == OrderId && !(Order.ShipDate > DateTime.Now || Order.ShipDate == DateTime.MinValue)))
        {
            throw new BO.OrderAlreadyExpeditedException("Order already expedited");
        }
        else
        {
            throw new BO.NoExistingItemException("Order Not exist");
        }
    }
    
    public BO.Order UpdadteOrderReceived(int OrderId)
    {
        if (Dal.Order.GetList().ToList().Exists(Order => Order.Id == OrderId && (Order.DeliveryDate > DateTime.Now || Order.DeliveryDate == DateTime.MinValue)))//  test if the Order With the specific OrderiD EXIST and already not Received
        {
            BO.Order order = new BO.Order();
            order=GetOrderItem(OrderId);
            order.DeliveryDate=DateTime.Now;
            order.Status = BO.OrderStatus.Received;
            foreach (var item in Dal.OrderItem.GetList().ToList())// we nead also to delete the order items of the previous order
            {
                    if (item.OrderId == OrderId)
                    {
                        Dal.OrderItem.Delete(item.ProductId, item.OrderId);
                    }
            }
            Dal.Order.Delete(OrderId,0);// the Order was delivered to the client so we nead to remove it from the orderList
            return order;
        }
        else if(Dal.Order.GetList().ToList().Exists(Order => Order.Id == OrderId && !(Order.DeliveryDate > DateTime.Now || Order.DeliveryDate == DateTime.MinValue)))
        {
            throw new BO.OrderAlreadyReceivedException("Order already received!");
        }
        else
        {
            throw new BO.NoExistingItemException("Order not exist");
        }
        // faire aussi try catch
    }

    public BO.OrderTracking TrackingOrder(int OrderId)
    {
        if (Dal.Order.GetList().ToList().Exists(Order => Order.Id == OrderId))// test if the order Exist on the list of order of the dal
        {
            BO.OrderTracking orderTracking = new BO.OrderTracking();
            orderTracking.ID = OrderId;
            int index = GetOrderList().ToList().FindIndex(Order => Order.ID == OrderId);
            orderTracking.Status = GetOrderList().ToList()[index].Status;// get the status value of the list of order of the bl
            Tuple<DateTime, string> description1 = new Tuple<DateTime, string>(GetOrderItem(OrderId).PaymentDate, "Order Appoved");
            Tuple<DateTime, string> description2 = new Tuple<DateTime, string>(GetOrderItem(OrderId).ShipDate, "Order Expedied");
            Tuple<DateTime, string> description3 = new Tuple<DateTime, string>(GetOrderItem(OrderId).DeliveryDate, "Order Received");
            orderTracking.OrderTrackingList.Add(description1);
            orderTracking.OrderTrackingList.Add(description2);
            orderTracking.OrderTrackingList.Add(description3);
            return orderTracking;
        }
        else
        {
            throw new BO.NoExistingItemException("Order not exist");
        }
    }


    public void UpdateOrder(int orderId)
    {
        throw new NotImplementedException();
    }

}
