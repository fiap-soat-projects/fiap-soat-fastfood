using Domain.Entities;
using Domain.Exceptions;

namespace Application.Exceptions;

public class CustomerNotFoundException : EntityNotFoundException<Customer>
{
    public CustomerNotFoundException(string id) : base(id)
    {

    }
}
