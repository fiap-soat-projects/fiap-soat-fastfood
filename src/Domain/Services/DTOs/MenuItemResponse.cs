using Domain.Entities.Enums;
using System.Text.Json.Serialization;

namespace Domain.Services.DTOs;

public record MenuItemResponse(
    string Id,
    string Name,
    decimal Price,
    MenuItemCategory Category,
    string Description,
    bool IsActive)
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public MenuItemCategory Category { get; init; } = Category;
}