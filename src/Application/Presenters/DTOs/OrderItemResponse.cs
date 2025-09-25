namespace Adapter.Presenters.DTOs;

public record OrderItemResponse
(
    string? Name,
    string? Category,
    decimal Price,
    int Amount
) { }