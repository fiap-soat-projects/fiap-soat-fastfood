namespace Application.UseCases.DTOs.Request;

public record OrderItemRequest
(
    string? Id,
    int Amount
)
{ }