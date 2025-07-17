using System.Text.Json.Serialization;

namespace Infrastructure.Gateways.Clients.DTOs;

internal class MercadoPagoPointOfInteraction
{
    [JsonPropertyName("transaction_data")]
    public MercadoPagoTransactionData? TransactionData { get; init; }
}
