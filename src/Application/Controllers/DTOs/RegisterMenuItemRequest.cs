using Business.Entities.Enums;

namespace Adapter.Controllers.DTOs;

public record RegisterMenuItemRequest(
    string? Name,
    decimal Price,
    ItemCategory Category,
    string? Description)
{
    public ItemCategory Category { get; init; } = Category;
}