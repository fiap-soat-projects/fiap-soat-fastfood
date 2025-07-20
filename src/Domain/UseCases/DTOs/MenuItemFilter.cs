using Business.Entities.Enums;

namespace Business.UseCases.DTOs;

internal record MenuItemFilter(
    string? Name,
    ItemCategory? Category,
    int Skip,
    int Limit);