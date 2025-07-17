using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Gateways.DTOs;

namespace Domain.Gateways.Interfaces;

public interface IPixGateway
{
    Task<PaymentCheckout> CreatePaymentAsync(
        CheckoutInput checkoutInput,
        PaymentMethod paymentMethod,
        CancellationToken cancellationToken);
}
