namespace BlApi;

public interface IBl   //Qu'on va utiliser dans le program.cs
{
    public IProduct Product { get; }
    public IOrder Order { get; }
    public IOrderItem OrderItem { get; }
    public ICart Cart { get; }
}
