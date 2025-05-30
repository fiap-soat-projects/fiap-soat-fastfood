using Domain.Entities.Enums;

namespace Domain.Services.DTOs;

public record MenuItemFilter(
    string? Name,
    MenuItemCategory? Category,
    int Skip,
    int Limit);