using Business.Entities;
using Business.Entities.Enums;
using Business.Gateways.Clients.DTOs;
using Business.Gateways.Clients.Interfaces;
using Business.Gateways.Repositories.Interfaces;
using Business.UseCases.Interfaces;

namespace Business.UseCases;
internal class TransactionUseCase : ITransactionUseCase
{
    private readonly IPixClient _pixClient;
    private readonly ICustomerUseCase _customerUseCase;
    private readonly IOrderRepository _orderRepository;

    public TransactionUseCase(IPixClient pixClient, ICustomerUseCase customerUseCase, IOrderRepository orderRepository)
    {
        _pixClient = pixClient;
        _customerUseCase = customerUseCase;
        _orderRepository = orderRepository;
    }

    public async Task<PaymentCheckout> CheckoutAsync(Order order, PaymentMethod method, CancellationToken cancellationToken)
    {
        order = await _orderRepository.UpdatePaymentMethodAsync(order.Id, method, cancellationToken);

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

        await _orderRepository.UpdateStatusAsync(id, OrderStatus.Received, cancellationToken);
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

        var orderPaymentCheckout = await _pixClient.CreatePaymentAsync(checkoutInput!, paymentMethod, cancellationToken);

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

        var orderPaymentCheckout = await _pixClient.CreatePaymentAsync(checkoutInput!, paymentMethod, cancellationToken);

        return orderPaymentCheckout;
    }
}
