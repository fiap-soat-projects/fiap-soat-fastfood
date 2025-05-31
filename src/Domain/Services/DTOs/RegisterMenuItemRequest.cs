using Domain.Entities.Enums;
using System.Text.Json.Serialization;

namespace Domain.Services.DTOs;

public record RegisterMenuItemRequest(
    string? Name,
    decimal Price,
    ItemCategory Category,
    string? Description)
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ItemCategory Category { get; init; } = Category;
}