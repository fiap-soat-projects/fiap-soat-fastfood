using Domain.Exceptions;

namespace Domain.Entities.Exceptions;

internal class CustomerException : InvalidEntityPropertyException<Customer>
{
    public CustomerException(string propertyName) : base(propertyName)
    {

    }
}