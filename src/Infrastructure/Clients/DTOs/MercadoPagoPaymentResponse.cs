using Business.Entities;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Infrastructure.Clients.DTOs;

[ExcludeFromCodeCoverage]
internal class MercadoPagoPaymentResponse
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("payment_method_id")]
    public string? PaymentMethodId { get; init; }

    [JsonPropertyName("transaction_amount")]
    public decimal TransactionAmount { get; init; }

    [JsonPropertyName("currency_id")]
    public string? CurrencyId { get; init; }

    [JsonPropertyName("metadata")]
    public MercadoPagoMetadata? Metadata { get; init; }

    [JsonPropertyName("point_of_interaction")]
    public MercadoPagoPointOfInteraction? PointOfInteraction { get; init; }

    internal PaymentCheckout ToDomain()
    {
        var orderPaymentCheckout = new PaymentCheckout
        {
            Id = Id,
            PaymentMethod = PaymentMethodId!,
            QrCode = PointOfInteraction?.TransactionData?.QrCode!,
            QrCodeBase64 = PointOfInteraction?.TransactionData?.QrCodeBase64!,
            Amount = TransactionAmount,
        };

        return orderPaymentCheckout;
    }
}
