using Domain.Adapters.DTOs;
using Domain.Entities;
using Domain.Entities.Enums;

namespace Domain.Adapters.Interfaces;

public interface IMercadoPagoPaymentAdapter
{
    Task<OrderPaymentCheckoutEntity> CreatePaymentAsync(
        CheckoutInput checkoutInput,
        PaymentMethod paymentMethod,
        CancellationToken cancellationToken);
}
