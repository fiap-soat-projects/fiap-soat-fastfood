using Domain.Entities;

namespace Infrastructure.Clients.DTOs.Extensions;

internal static class MercadoPagoPaymentResponseExtensions
{
    internal static PaymentCheckout ToOrderPaymentCheckoutEntity(this MercadoPagoPaymentResponse response)
    {
        var orderPaymentCheckout = new PaymentCheckout
        {
            Id = response.Id!,
            PaymentMethod = response.PaymentMethodId!,
            QrCode = response.PointOfInteraction?.TransactionData?.QrCode!,
            QrCodeBase64 = response.PointOfInteraction?.TransactionData?.QrCodeBase64!,
            Amount = response.TransactionAmount!,
        };

        return orderPaymentCheckout;
    }
}
