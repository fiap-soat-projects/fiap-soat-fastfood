namespace Adapter.Presenters;

public record OrderItemResponse
(
    string? Name,
    string? Category,
    decimal Price,
    int Amout
) { }