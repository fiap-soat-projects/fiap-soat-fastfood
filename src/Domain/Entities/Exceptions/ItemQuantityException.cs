using Domain.Exceptions;

namespace Domain.Entities.Exceptions;

public class ItemQuantityException : InvalidEntityPropertyException<ItemQuantity>
{
    protected ItemQuantityException(string propertyName) : base(propertyName)
    {

    }
}
