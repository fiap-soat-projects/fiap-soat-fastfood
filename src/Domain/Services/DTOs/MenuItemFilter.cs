using Domain.Entities.Enums;

namespace Domain.Services.DTOs;

public record MenuItemFilter(
    string? Name,
    ItemCategory? Category,
    int Skip,
    int Limit);