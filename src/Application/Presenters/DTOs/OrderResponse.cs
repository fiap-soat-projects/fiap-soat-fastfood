using Business.Entities;

namespace Adapter.Presenters.DTOs;

public record OrderResponse
(
    string Id,
    string CustomerId,
    string CustomerName,
    IEnumerable<OrderItemResponse> Items,
    string Status,
    Payment? payment,
    decimal TotalPrice
) { }
