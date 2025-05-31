using Domain.Exceptions;

namespace Domain.Entities.Exceptions;

public class OrderException : InvalidEntityPropertyException<Order>
{
    public OrderException(string propertyName) : base(propertyName)
    {

    }
}
