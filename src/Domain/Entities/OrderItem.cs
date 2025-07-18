using Business.Entities.Enums;
using Business.Entities.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Business.Entities;

public class OrderItem
{
    private string? _id;
    private string? _name;
    private ItemCategory _category;
    private decimal _price;
    private int _amount;

    public required string Id
    {
        get => _id!;
        set
        {
            OrderItemException.ThrowIfNullOrWhiteSpace(value, nameof(Id));

            _id = value;
        }
    }

    public required string Name
    {
        get => _name!;
        set
        {
            OrderItemException.ThrowIfNullOrWhiteSpace(value, nameof(Name));

            _name = value;
        }
    }

    public required ItemCategory Category
    {
        get => _category;
        set
        {
            ValidateCategory(value);

            _category = value;
        }
    }

    public required decimal Price
    {
        get => _price;
        set
        {
            OrderItemException.ThrowIfIsEqualOrLowerThanZero(value, nameof(Price));

            _price = value;
        }
    }

    public required int Amount
    {
        get => _amount;
        set
        {
            OrderItemException.ThrowIfIsEqualOrLowerThanZero(value, nameof(Amount));

            _amount = value;
        }
    }

    [SetsRequiredMembers]
    internal OrderItem(string id, string name, ItemCategory category, decimal price, int amount)
    {
        Id = id;
        Name = name;
        Category = category;
        Price = price;
        Amount = amount;
    }

    internal decimal GetTotalPrice()
    {
        return Price * Amount;
    }

    private static void ValidateCategory(ItemCategory value)
    {
        var isInvalidCategory = !Enum.IsDefined(typeof(ItemCategory), value) || value == ItemCategory.None;

        if (isInvalidCategory)
        {
            throw new OrderItemException(nameof(Category));
        }
    }
}
