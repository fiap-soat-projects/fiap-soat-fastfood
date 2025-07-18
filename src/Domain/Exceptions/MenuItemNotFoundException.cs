using Business.Entities;

namespace Business.Exceptions;

public class MenuItemNotFoundException : EntityNotFoundException<MenuItem>
{
    public MenuItemNotFoundException(string id) : base(id)
    {

    }
}
