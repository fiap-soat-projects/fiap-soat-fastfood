using Domain.Entities;
using Domain.Exceptions;

namespace Application.Exceptions;

public class CustomerNotFoundException : EntityNotFoundException<Customer>
{
    public CustomerNotFoundException(string id) : base(id)
    {

    }

    public static void ThrowIfNull(Customer? customer, string identifier)
    {
        if (customer is null)
        {
            throw new CustomerNotFoundException(identifier);
        }
    }
}
