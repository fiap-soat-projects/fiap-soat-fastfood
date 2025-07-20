using Business.Exceptions;

namespace Business.Entities.Exceptions;

public class ItemQuantityException : InvalidEntityPropertyException<ItemQuantity>
{
    protected ItemQuantityException(string propertyName) : base(propertyName)
    {

    }
}
