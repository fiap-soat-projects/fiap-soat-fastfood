namespace Application.UseCases.DTOs.Request;

public record OrderItemRequest
(
    string? Id,
    string? Name,
    string? Category, 
    decimal Price, 
    int Amount
) { }