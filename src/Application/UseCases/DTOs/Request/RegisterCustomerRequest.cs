using System.Diagnostics.CodeAnalysis;

namespace Application.UseCases.DTOs.Request;

[ExcludeFromCodeCoverage]
public record class RegisterCustomerRequest
{
    public required string Name { get; init; }
    public required string Cpf { get; init; }
    public required string Email { get; init; }
}
