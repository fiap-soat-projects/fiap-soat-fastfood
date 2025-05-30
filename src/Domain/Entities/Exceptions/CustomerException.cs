using Domain.Exceptions;

namespace Domain.Entities.Exceptions;

public class CustomerException : InvalidEntityPropertyException<Customer>
{
    public CustomerException(string propertyName) : base(propertyName)
    {

    }
}