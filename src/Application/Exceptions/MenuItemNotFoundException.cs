using Domain.Entities;
using Domain.Exceptions;

namespace Application.Exceptions;

public class MenuItemNotFoundException : EntityNotFoundException<MenuItem>
{
    public MenuItemNotFoundException(string id) : base(id)
    {

    }
}
