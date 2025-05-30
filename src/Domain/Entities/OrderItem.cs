using Domain.Entities.Enums;
using Domain.Entities.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities;

public class OrderItem
{
    private string? _id;
    private string? _name;
    private MenuItemCategory _category;
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

    public required MenuItemCategory Category
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
    internal OrderItem(string id, string name, MenuItemCategory category, decimal price, int amount)
    {
        Id = id;
        Name = name;
        Category = category;
        Price = price;
        Amount = amount;
    }

    public decimal GetTotalPrice()
    {
        return Price * Amount;
    }

    private static void ValidateCategory(MenuItemCategory value)
    {
        var isInvalidCategory = !Enum.IsDefined(typeof(MenuItemCategory), value) || value == MenuItemCategory.None;

        if (isInvalidCategory)
        {
            throw new OrderItemException(nameof(Category));
        }
    }
}
