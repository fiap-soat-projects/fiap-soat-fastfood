namespace Application.DTOs;

public record OrderItemRequest
(
    string? Id,
    string? Name,
    string? Category, 
    decimal Price, 
    int Amount
) { }