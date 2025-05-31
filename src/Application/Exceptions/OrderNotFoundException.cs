using Domain.Entities;
using Domain.Exceptions;

namespace Application.Exceptions;

public class OrderNotFoundException : EntityNotFoundException<Order>
{
    protected OrderNotFoundException(string id) : base(id)
    {

    }
}

