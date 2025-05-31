using Domain.Entities;
using Domain.Entities.Enums;

namespace Domain.Services.Interfaces;
internal interface ITransactionService
{
    Task<PaymentCheckout> CheckoutAsync(Order order, PaymentMethod method, CancellationToken cancellationToken);
    Task ConfirmPaymentAsync(string orderId, CancellationToken cancellationToken);
}
