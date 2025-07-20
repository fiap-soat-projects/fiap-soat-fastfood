using Business.Exceptions;

namespace Business.Entities.Exceptions;

public class MenuItemException : InvalidEntityPropertyException<MenuItem>
{
    public MenuItemException(string propertyName) : base(propertyName)
    {

    }

    public static void ThrowIfExceedsMaxLength(string value, string propertyName, int maxLength)
    {
        if (value.Length > maxLength)
        {
            throw new MenuItemException(propertyName);
        }
    }

    public static void ThrowIfZero(decimal value, string propertyName)
    {
        if (value == 0)
        {
            throw new MenuItemException(propertyName);
        }
    }

    public static void ThrowIfNegative(decimal value, string propertyName)
    {
        if (value < 0)
        {
            throw new MenuItemException(propertyName);
        }
    }
}