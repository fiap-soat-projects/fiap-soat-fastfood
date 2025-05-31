using Domain.Entities.Enums;

namespace Application.UseCases.DTOs.Request;

public record UpdateMenuItemRequest(
    string? Name,
    decimal Price,
    ItemCategory Category,
    string? Description,
    bool IsActive)
{
    public ItemCategory Category { get; init; } = Category;
}