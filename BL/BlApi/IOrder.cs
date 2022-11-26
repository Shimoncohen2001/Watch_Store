
namespace BlApi;

/// <summary>
/// ////////////////////////IBoOrder Interface///////////////////////
/// </summary>
public interface IOrder
{
    public IEnumerable<BO.OrderForList> GetOrderList();
    public BO.Order GetOrderItem(int OrderId);
    public BO.Order UpdateOrderShipping(int OrderId);
    public BO.Order UpdadteOrderReceived(int OrderId);
    public BO.OrderTracking TrackingOrder(int OrderId);
    public void UpdateOrder();
}
