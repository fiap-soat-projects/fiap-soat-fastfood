using Domain.Adapters.DTOs;
using Domain.Adapters.Interfaces;
using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Repositories.Interfaces;
using Domain.Services.Interfaces;

namespace Application.Services;
internal class PaymentService : IPaymentService
{
    private readonly IPixAdapter _pixAdapter;
    private readonly ICustomerService _customerService;
    private readonly IOrderRepository _orderRepository;

    public PaymentService(IPixAdapter pixAdapter, ICustomerService registerCustomerService, IOrderRepository orderRepository)
    {
        _pixAdapter = pixAdapter;
        _customerService = registerCustomerService;
        _orderRepository = orderRepository;
    }

    public async Task<PaymentCheckout> CheckoutAsync(Order order, PaymentMethod method, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(order?.CustomerId))
        {
            return await ExecuteCustomerCheckoutAsync(order!, method, cancellationToken);
        }

        var orderPaymentCheckout = await ExecuteAnonymousCheckoutAsync(order!, method, cancellationToken);
        return orderPaymentCheckout;
    }

    public async Task ConfirmPaymentAsync(string id, CancellationToken cancellationToken)
    {
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

        var orderPaymentCheckout = await _pixAdapter.CreatePaymentAsync(checkoutInput!, paymentMethod, cancellationToken);
        return orderPaymentCheckout;
    }

    private async Task<PaymentCheckout> ExecuteCustomerCheckoutAsync(Order order, PaymentMethod paymentMethod, CancellationToken cancellationToken)
    {
        var customer = await _customerService.GetByIdAsync(order!.CustomerId!, cancellationToken);

        var checkoutInput = new CheckoutInput
        (
            customer.Id!,
            order.CustomerName!,
            customer.Email!,
            order.Id!,
            order.TotalPrice
        );

        var orderPaymentCheckout = await _pixAdapter.CreatePaymentAsync(checkoutInput!, paymentMethod, cancellationToken);

        return orderPaymentCheckout;
    }
}
