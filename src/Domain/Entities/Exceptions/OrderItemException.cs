using Domain.Exceptions;

namespace Domain.Entities.Exceptions;

public class OrderItemException : InvalidEntityPropertyException<OrderItem>
{
    public OrderItemException(string propertyName) : base(propertyName)
    {

    }
}
