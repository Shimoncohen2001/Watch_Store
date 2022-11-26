﻿using BlApi;
using BO;
using Dal;
using DalApi;
using DO;
using System.Net.Mail;
using System.Runtime.InteropServices;

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
            foreach (var item1 in Dal.OrderItem.GetList())
            {
                if (item1.OrderId==item.Id)
                {
                    amountOfItems+=item1.Amount;
                    totalPrice+=amountOfItems*(item1.Price);
                }
            }
            if (item.OrderDate<=DateTime.Now && item.ShipDate>DateTime.Now)
            {
                 status = 0;
            }
            else if(item.ShipDate<=DateTime.Now && item.DeliveryDate>DateTime.Now)
            {
                status = 1;
            }
            else if (item.DeliveryDate<=DateTime.Now)
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
                            double price=Convert.ToDouble(item1.Price);
                            int Amount=Convert.ToInt32(item1.Amount);
                            BO.OrderItem orderItem=new BO.OrderItem() { Name=productName,
                                Price=price,
                                Amount=Amount,
                                ProductID=ProductId,
                                ID=OrderitemId,
                                TotalPrice=Amount*price
                                };
                            orderItemsList.Add(orderItem);
                            totalPrice += Dal.Product.Get(item1.ProductId, 0).Price * item1.Amount;
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
                    if(order.ShipDate>DateTime.Now)
                    {
                        order.Status=BO.OrderStatus.Approved;
                    }
                    else if (order.ShipDate<DateTime.Now && order.DeliveryDate>DateTime.Now)
                    {
                        order.Status = BO.OrderStatus.Expedited;
                    }
                    else if (order.DeliveryDate<=DateTime.Now)
                    {
                        order.Status = BO.OrderStatus.Received;
                    }
                    return order;
                }
            }
            throw new Exception("NO existing item");
        }
        else
        {
            throw new Exception("invalid Id");
        }
        // faire aussi try catch
    }
    
    public BO.Order UpdateOrderShipping(int OrderId)
    {
        if (Dal.Order.GetList().ToList().Exists(Order => Order.Id == OrderId&&Order.ShipDate>DateTime.Now))// test if the Order With the specific OrderiD EXIST and already not sent
        {
            GetOrderItem(OrderId).ShipDate = DateTime.Now;// update the order shipping date
            GetOrderItem(OrderId).Status = BO.OrderStatus.Expedited;
            DO.Order order = new DO.Order() { Id = Dal.Order.Get(OrderId, 0).Id,
                CustomerAdress = Dal.Order.Get(OrderId, 0).CustomerAdress,
                CustomerEmail = Dal.Order.Get(OrderId, 0).CustomerEmail,
                CustomerName = Dal.Order.Get(OrderId, 0).CustomerName,
                DeliveryDate = Dal.Order.Get(OrderId, 0).DeliveryDate,
                OrderDate = Dal.Order.Get(OrderId, 0).OrderDate,
                ShipDate = DateTime.Now
            };
            Dal.Order.Delete(OrderId, 0);//remove on the dal
            Dal.Order.Add(order);// Add the updated Order to the dal
            return GetOrderItem(OrderId);
        }
        else
        {
            throw new Exception("No Existing Items");
        }
        //faire aussi try catch
    }
    
    public BO.Order UpdadteOrderReceived(int OrderId)
    {
        if (Dal.Order.GetList().ToList().Exists(Order=>Order.Id==OrderId&&(Order.ShipDate < DateTime.Now && Order.DeliveryDate > DateTime.Now)))//  test if the Order With the specific OrderiD EXIST and already not Received
        {
            BO.Order order = new BO.Order();
            order=GetOrderItem(OrderId);
            order.DeliveryDate=DateTime.Now;
            order.Status = BO.OrderStatus.Received;
            Dal.Order.Delete(OrderId,0);// the Order was delivered to the client so we nead to remove it from the orderList
            return order;
        }
        else
        {
            throw new Exception("No Existing Item");
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
            throw new Exception("No existing item");
            // ne pas oublier de fwaire la hariga de noExisting items
        }
        // faire aussi try catch
    }


    public void UpdateOrder()
    {

        throw new NotImplementedException();
    }

}
