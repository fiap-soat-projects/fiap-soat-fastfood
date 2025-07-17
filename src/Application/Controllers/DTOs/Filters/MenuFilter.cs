using Domain.Entities.Enums;

namespace Application.Controllers.DTOs.Filters;

public record MenuFilter(
    string? Name,
    ItemCategory? Category,
    int Skip,
    int Limit);