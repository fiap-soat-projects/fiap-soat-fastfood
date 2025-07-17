namespace Application.Controllers.DTOs.Response;

public record OrderItemResponse
(
    string? Name,
    string? Category,
    decimal Price,
    int Amout
) { }