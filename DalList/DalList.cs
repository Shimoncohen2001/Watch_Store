using DalApi;


namespace Dal;

sealed internal class DalList : IDal
{
    //internal static DalList? inst = null;

    //private static readonly object padlock = new object();

    //internal static DalList Inst
    //{
    //    get
    //    {
    //        lock(padlock)
    //        {
    //            if (inst==null)
    //            {
    //                inst = new DalList();
    //            }
    //            return inst;
    //        }
    //    }
    //}


    private DalList() { }
    public IProduct Product => new DalProducts();

    public IOrder Order => new DalOrder();

    public IOrderItem OrderItem => new DalOrderItems();

    public static IDal Instance { get; } = new DalList();
}


