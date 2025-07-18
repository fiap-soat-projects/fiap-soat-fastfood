using Business.Entities.Enums;

namespace Adapter.Controllers.DTOs;

public record UpdateMenuItemRequest(
    string? Name,
    decimal Price,
    ItemCategory Category,
    string? Description,
    bool IsActive)
{
    public ItemCategory Category { get; init; } = Category;
}