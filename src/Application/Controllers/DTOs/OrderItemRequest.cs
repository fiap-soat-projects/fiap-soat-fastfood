namespace Adapter.Controllers.DTOs;

public record OrderItemRequest
(
    string? Id,
    int Amount
)
{ }