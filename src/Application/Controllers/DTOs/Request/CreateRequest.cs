namespace Application.Controllers.DTOs.Request;

public record CreateRequest
(
    string? CustomerId, 
    string? CustomerName,
    IEnumerable<OrderItemRequest> Items
) { }