namespace Application.DTOs;

public record OrderItemResponse
(
    string? Name,
    string? Category,
    decimal Price,
    int Amout
) { }