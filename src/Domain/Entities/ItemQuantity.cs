using Business.Entities.Exceptions;

namespace Business.Entities;

public class ItemQuantity
{
    private readonly string? _itemId;
    private readonly int _quantity;

    public required string ItemId
    {
        get => _itemId!;
        init
        {
            ItemQuantityException.ThrowIfNullOrWhiteSpace(value, nameof(ItemId));

            _itemId = value;
        }
    }

    public required int Quantity
    {
        get => _quantity;
        init
        {
            ItemQuantityException.ThrowIfIsEqualOrLowerThanZero(value, nameof(Quantity));

            _quantity = value;
        }
    }

    public override string ToString()
    {
        return $"Item: {_itemId} - Quantity: {_quantity}";
    }
}
