namespace Application.Controllers.DTOs.Response;

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
