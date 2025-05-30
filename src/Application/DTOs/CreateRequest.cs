namespace Application.DTOs;

public record CreateRequest
(
    string? CustomerId, 
    string? CustomerName,
    IEnumerable<OrderItemRequest> Items
) { }