using Business.Entities.Enums;

namespace Adapter.Controllers.DTOs.Filters;

public record MenuFilter(
    string? Name,
    ItemCategory? Category,
    int Skip,
    int Limit);