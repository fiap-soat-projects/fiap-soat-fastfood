using System.Diagnostics.CodeAnalysis;

namespace Adapter.Presenters.DTOs;

[ExcludeFromCodeCoverage]
public record RegisterCustomerResponse
{
    public required string Id { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required string Name { get; init; }
    public required string Cpf { get; init; }
    public required string Email { get; init; }
}
