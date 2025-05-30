using CrossCutting.Exceptions;
using Domain.Entities;

namespace Domain.Entities.Exceptions;

public class InvalidInventoryItemPropertyException(string message) : BaseEntityException<ItemQuantity>(message)
{
    public static void ThrowIfIsNullOrWhiteSpace(string? value, string propertyName, Type type)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidInventoryItemPropertyException($"The property {propertyName} from {type} can't be null or white space");
        }
    }

    public static void ThrowIfIsEqualOrLowerThanZero(int value, string propertyName, Type type)
    {
        if (value <= 0)
        {
            throw new InvalidInventoryItemPropertyException($"The property {propertyName} from {type} can't be equals or lower than zero");
        }
    }
}
