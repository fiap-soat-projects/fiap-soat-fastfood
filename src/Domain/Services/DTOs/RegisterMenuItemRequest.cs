using Domain.Entities.Enums;
using System.Text.Json.Serialization;

namespace Domain.Services.DTOs;

public record RegisterMenuItemRequest(
    string? Name,
    decimal Price,
    MenuItemCategory Category,
    string? Description)
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public MenuItemCategory Category { get; init; } = Category;
}