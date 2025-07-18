using Business.Exceptions;

namespace Business.Entities.Exceptions;

public class OrderException : InvalidEntityPropertyException<Order>
{
    public OrderException(string propertyName) : base(propertyName)
    {

    }
}
