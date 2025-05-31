using Domain.Entities;
using Domain.Exceptions;

namespace Domain.Services.Exceptions;

internal class OrderNotFoundException : EntityNotFoundException<Order>
{
    protected OrderNotFoundException(string id) : base(id)
    {

    }
}

