namespace Domain.Services.DTOs;
public record OrderFilter (string? Status, int Page, int Size)
{
}
