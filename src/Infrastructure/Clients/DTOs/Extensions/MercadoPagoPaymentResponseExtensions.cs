using Domain.Entities;

namespace Infrastructure.Clients.DTOs.Extensions;

internal static class MercadoPagoPaymentResponseExtensions
{
    internal static OrderPaymentCheckoutEntity ToOrderPaymentCheckoutEntity(this MercadoPagoPaymentResponse response)
    {
        var orderPaymentCheckout = new OrderPaymentCheckoutEntity
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
