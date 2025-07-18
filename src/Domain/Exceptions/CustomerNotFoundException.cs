using Business.Entities;

namespace Business.Exceptions;

public class CustomerNotFoundException : EntityNotFoundException<Customer>
{
    public CustomerNotFoundException(string id) : base(id)
    {

    }
}
