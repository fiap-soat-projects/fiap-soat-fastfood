using Business.Entities;

namespace Business.Exceptions;

public class OrderNotFoundException : EntityNotFoundException<Order>
{
    protected OrderNotFoundException(string id) : base(id)
    {

    }
}

