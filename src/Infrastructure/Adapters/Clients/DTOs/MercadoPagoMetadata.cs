using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Infrastructure.Adapters.Clients.DTOs;

[ExcludeFromCodeCoverage]
internal class MercadoPagoMetadata
{
    [JsonPropertyName("order_number")]
    public string? OrderNumber { get; init; }
}
