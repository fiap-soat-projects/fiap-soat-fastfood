using Domain.Entities;
using Domain.Entities.Enums;

namespace Domain.UseCases.Interfaces;
internal interface ITransactionUseCase
{
    Task<PaymentCheckout> CheckoutAsync(Order order, PaymentMethod method, CancellationToken cancellationToken);
    Task ConfirmPaymentAsync(string orderId, CancellationToken cancellationToken);
}
