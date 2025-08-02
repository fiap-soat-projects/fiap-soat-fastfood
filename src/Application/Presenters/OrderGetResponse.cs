using Business.Entities;

namespace Adapter.Presenters;

public record OrderGetResponse
(
    string Id,
    string CustomerId,
    string CustomerName,
    IEnumerable<OrderItemResponse> Items,
    string Status,
    Payment? payment,
    decimal TotalPrice
) { }
