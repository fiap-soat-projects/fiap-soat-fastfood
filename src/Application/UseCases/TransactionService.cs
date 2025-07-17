using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Gateways.DTOs;
using Domain.Gateways.Interfaces;
using Domain.UseCases.Interfaces;

namespace Application.UseCases;
internal class TransactionService : ITransactionUseCase
{
    private readonly IPixGateway _pixGateway;
    private readonly ICustomerUseCase _customerUseCase;
    private readonly IOrderGateway _orderGateway;

    public TransactionService(IPixGateway pixAdapter, ICustomerUseCase registerCustomerService, IOrderGateway orderRepository)
    {
        _pixGateway = pixAdapter;
        _customerUseCase = registerCustomerService;
        _orderGateway = orderRepository;
    }

    public async Task<PaymentCheckout> CheckoutAsync(Order order, PaymentMethod method, CancellationToken cancellationToken)
    {
        order = await _orderGateway.UpdatePaymentMethodAsync(order.Id, method, cancellationToken);

        if (!string.IsNullOrWhiteSpace(order?.CustomerId))
        {
            return await ExecuteCustomerCheckoutAsync(order!, method, cancellationToken);
        }

        var orderPaymentCheckout = await ExecuteAnonymousCheckoutAsync(order!, method, cancellationToken);

        return orderPaymentCheckout;
    }

    public async Task ConfirmPaymentAsync(string id, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id, nameof(id));

        await _orderGateway.UpdateStatusAsync(id, OrderStatus.Received, cancellationToken);
    }

    private async Task<PaymentCheckout> ExecuteAnonymousCheckoutAsync(Order order, PaymentMethod paymentMethod, CancellationToken cancellationToken)
    {
        var checkoutInput = new CheckoutInput
        (
            "NoUser",
            "fakeUserName",
            "fakeemail@fake.com",
            order.Id!,
            order.TotalPrice
        );

        var orderPaymentCheckout = await _pixGateway.CreatePaymentAsync(checkoutInput!, paymentMethod, cancellationToken);

        return orderPaymentCheckout;
    }

    private async Task<PaymentCheckout> ExecuteCustomerCheckoutAsync(Order order, PaymentMethod paymentMethod, CancellationToken cancellationToken)
    {
        var customer = await _customerUseCase.GetByIdAsync(order!.CustomerId!, cancellationToken);

        var checkoutInput = new CheckoutInput
        (
            customer.Id!,
            order.CustomerName!,
            customer.Email!,
            order.Id!,
            order.TotalPrice
        );

        var orderPaymentCheckout = await _pixGateway.CreatePaymentAsync(checkoutInput!, paymentMethod, cancellationToken);

        return orderPaymentCheckout;
    }
}
