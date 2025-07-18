using Business.Entities.Enums;
using Business.Entities.Exceptions;
using Business.Entities.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace Business.Entities;

public class MenuItem : IAggregateRoot
{
    private string? _id;
    private string? _name;
    private decimal _price;
    private string? _description;

    public DateTime CreatedAt { get; init; }
    public bool IsActive { get; internal set; }
    public required ItemCategory Category { get; init; }

    public string Id
    {
        get => _id!;
        set
        {
            MenuItemException.ThrowIfNullOrWhiteSpace(value, nameof(Id));

            _id = value;
        }
    }

    public required string Name
    {
        get => _name!;
        set
        {
            MenuItemException.ThrowIfNullOrWhiteSpace(value, nameof(Name));

            _name = value;
        }
    }

    public required decimal Price
    {
        get => _price;
        set
        {
            MenuItemException.ThrowIfZero(value, nameof(Name));
            MenuItemException.ThrowIfNegative(value, nameof(Name));

            _price = value;
        }
    }

    public required string Description
    {
        get => _description!;
        set
        {
            MenuItemException.ThrowIfNullOrWhiteSpace(value, nameof(Description));

            _description = value;
        }
    }

    [SetsRequiredMembers]
    public MenuItem(string name, decimal price, string description, ItemCategory category)
    {
        Name = name;
        Price = price;
        Category = category;
        Description = description;
    }

    public MenuItem(string id, DateTime createdAt, bool isActive)
    {
        Id = id;
        CreatedAt = createdAt;
        IsActive = isActive;
    }

    internal void SetInactive()
    {
        IsActive = false;
    }
}
