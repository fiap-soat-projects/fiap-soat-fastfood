namespace Domain.Entities.Exceptions;
public class CustomerNotFoundException : Exception
{
    private const string DEFAULT_MESSAGE = "Customer with ID '{0}' not found";

    public CustomerNotFoundException(string customerId) 
        : base(string.Format(DEFAULT_MESSAGE, customerId))
    {
    }
}
