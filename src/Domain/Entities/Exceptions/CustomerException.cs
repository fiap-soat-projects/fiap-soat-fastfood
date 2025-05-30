namespace Domain.Entities.Exceptions;

public class CustomerException : BaseEntityException<Customer>
{
    public CustomerException(string propertyName) : base(propertyName)
    {
    }
}