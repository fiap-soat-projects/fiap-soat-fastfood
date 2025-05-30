namespace Application.DTOs;

public record RegisterCustomerRequest
{
    public string? CPF { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
}