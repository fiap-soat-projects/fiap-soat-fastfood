namespace Application.Controllers.DTOs.Request;

public record OrderItemRequest
(
    string? Id,
    int Amount
)
{ }