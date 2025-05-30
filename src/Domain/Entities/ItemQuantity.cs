using Domain.Entities.Exceptions;

namespace Domain.Entities;

public class ItemQuantity
{
    private readonly string? _itemId;
    private readonly int _quantity;

    public required string ItemId
    {
        get => _itemId!;
        init
        {
            InvalidInventoryItemPropertyException.ThrowIfIsNullOrWhiteSpace(
                value,
                nameof(ItemId),
                GetType());

            _itemId = value;
        }
    }

    public required int Quantity
    {
        get => _quantity;
        init
        {
            InvalidInventoryItemPropertyException.ThrowIfIsEqualOrLowerThanZero(
                value,
                nameof(Quantity),
                GetType());

            _quantity = value;
        }
    }

    public override string ToString()
    {
        return $"Item: {_itemId} - Quantity: {_quantity}";
    }
}
