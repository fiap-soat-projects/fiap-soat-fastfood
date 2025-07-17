using Domain.Entities.Enums;

namespace Domain.UseCases.DTOs;

internal record MenuItemFilter(
    string? Name,
    ItemCategory? Category,
    int Skip,
    int Limit);