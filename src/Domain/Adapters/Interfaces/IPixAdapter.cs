using Domain.Adapters.DTOs;
using Domain.Entities;
using Domain.Entities.Enums;

namespace Domain.Adapters.Interfaces;

public interface IPixAdapter
{
    Task<PaymentCheckout> CreatePaymentAsync(
        CheckoutInput checkoutInput,
        PaymentMethod paymentMethod,
        CancellationToken cancellationToken);
}
