using Business.Entities;

namespace Adapter.Presenters.DTOs;

public record UpdateOrderResponse
(
    string Id,
    string CustomerId,
    string CustomerName,
    IEnumerable<OrderItemResponse> Items,
    string Status,
    Payment? Payment,
    decimal TotalPrice
)
{ }
