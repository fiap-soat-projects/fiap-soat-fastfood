using Domain.Entities.Enums;
using System.Text.Json.Serialization;

namespace Domain.Services.DTOs;

public record UpdateMenuItemRequest(
    string? Name,
    decimal Price,
    ItemCategory Category,
    string? Description,
    bool IsActive)
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ItemCategory Category { get; init; } = Category;
}