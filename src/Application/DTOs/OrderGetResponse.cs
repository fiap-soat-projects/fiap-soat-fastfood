namespace Application.DTOs;

public record OrderGetResponse
(
    string Id,
    string CustomerId,
    string CustomerName,
    IEnumerable<OrderItemResponse> Items,
    string Status,
    string PaymentMethod,
    decimal TotalPrice
) { }
