using Business.Entities;
using Business.Exceptions;

namespace Business.Entities.Exceptions;

public class OrderItemException : InvalidEntityPropertyException<OrderItem>
{
    public OrderItemException(string propertyName) : base(propertyName)
    {

    }
}
