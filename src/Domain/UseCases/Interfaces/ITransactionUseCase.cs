using Business.Entities;
using Business.Entities.Enums;

namespace Business.UseCases.Interfaces;
internal interface ITransactionUseCase
{
    Task<PaymentCheckout> CheckoutAsync(Order order, PaymentMethod method, CancellationToken cancellationToken);
    Task ConfirmPaymentAsync(string orderId, CancellationToken cancellationToken);
    Task ProcessPaymentAsync(string orderId, Payment payment, CancellationToken cancellationToken);
}
