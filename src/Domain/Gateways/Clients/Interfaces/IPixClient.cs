using Business.Entities;
using Business.Entities.Enums;
using Business.Gateways.Clients.DTOs;

namespace Business.Gateways.Clients.Interfaces;

public interface IPixClient
{
    Task<PaymentCheckout> CreatePaymentAsync(
        CheckoutInput checkoutInput,
        PaymentMethod paymentMethod,
        CancellationToken cancellationToken);
}
