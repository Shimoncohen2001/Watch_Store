using System.Runtime.Serialization;



namespace BO;





[Serializable]

internal class NoExistingItemException : Exception

{

    public NoExistingItemException() : base() { }

    public NoExistingItemException(string message) : base(message) { }

    public NoExistingItemException(string message, Exception inner) : base(message, inner) { }

    protected NoExistingItemException(SerializationInfo info, StreamingContext context) : base(info, context) { }

}



[Serializable]

internal class ItemAlreadyExistException : Exception

{

    public ItemAlreadyExistException() : base() { }

    public ItemAlreadyExistException(string message) : base(message) { }

    public ItemAlreadyExistException(string message, Exception inner) : base(message, inner) { }

    protected ItemAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }

}



[Serializable]

internal class IdNotValidExcpetion : Exception

{

    public IdNotValidExcpetion() : base() { }

    public IdNotValidExcpetion(string message) : base(message) { }

    public IdNotValidExcpetion(string message, Exception inner) : base(message, inner) { }

    protected IdNotValidExcpetion(SerializationInfo info, StreamingContext context) : base(info, context) { }

}



[Serializable]

internal class NameNotValidExcpetion : Exception

{

    public NameNotValidExcpetion() : base() { }

    public NameNotValidExcpetion(string message) : base(message) { }

    public NameNotValidExcpetion(string message, Exception inner) : base(message, inner) { }

    protected NameNotValidExcpetion(SerializationInfo info, StreamingContext context) : base(info, context) { }

}



[Serializable]

internal class PriceNotValidExcpetion : Exception

{

    public PriceNotValidExcpetion() : base() { }

    public PriceNotValidExcpetion(string message) : base(message) { }

    public PriceNotValidExcpetion(string message, Exception inner) : base(message, inner) { }

    protected PriceNotValidExcpetion(SerializationInfo info, StreamingContext context) : base(info, context) { }

}



[Serializable]

internal class StockNotValidExcpetion : Exception

{

    public StockNotValidExcpetion() : base() { }

    public StockNotValidExcpetion(string message) : base(message) { }

    public StockNotValidExcpetion(string message, Exception inner) : base(message, inner) { }

    protected StockNotValidExcpetion(SerializationInfo info, StreamingContext context) : base(info, context) { }

}



[Serializable]

internal class DeleteItemNotValidExcpetion : Exception

{

    public DeleteItemNotValidExcpetion() : base() { }

    public DeleteItemNotValidExcpetion(string message) : base(message) { }

    public DeleteItemNotValidExcpetion(string message, Exception inner) : base(message, inner) { }

    protected DeleteItemNotValidExcpetion(SerializationInfo info, StreamingContext context) : base(info, context) { }

}