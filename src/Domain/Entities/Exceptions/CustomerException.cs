using Business.Entities;
using Business.Exceptions;

namespace Business.Entities.Exceptions;

internal class CustomerException : InvalidEntityPropertyException<Customer>
{
    public CustomerException(string propertyName) : base(propertyName)
    {

    }
}