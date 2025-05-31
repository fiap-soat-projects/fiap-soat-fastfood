using Domain.Entities.Enums;

namespace Domain.Services.DTOs;

internal record MenuItemFilter(
    string? Name,
    ItemCategory? Category,
    int Skip,
    int Limit);