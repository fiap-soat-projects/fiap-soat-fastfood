using Domain.Entities.Enums;

namespace Application.UseCases.DTOs.Response;

public record MenuItemResponse(
    string Id,
    string Name,
    decimal Price,
    ItemCategory Category,
    string Description,
    bool IsActive)
{
    public ItemCategory Category { get; init; } = Category;
}