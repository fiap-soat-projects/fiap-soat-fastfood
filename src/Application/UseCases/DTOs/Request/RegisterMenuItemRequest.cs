using Domain.Entities.Enums;

namespace Application.UseCases.DTOs.Request;

public record RegisterMenuItemRequest(
    string? Name,
    decimal Price,
    ItemCategory Category,
    string? Description)
{
    public ItemCategory Category { get; init; } = Category;
}