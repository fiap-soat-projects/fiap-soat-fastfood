using Domain.Adapters.DTOs;
using Domain.Adapters.Interfaces;
using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Repositories.Interfaces;
using Domain.Services.DTOs;
using Domain.Services.DTOs.Extensions;
using Domain.Services.Exceptions;
using Domain.Services.Interfaces;


namespace Application.Services;
internal class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IInventoryService _inventoryService;
    private readonly IRegisterCustomerService _registerCustomerService;
    private readonly IMercadoPagoPaymentAdapter _mercadoPagoPaymentAdapter;

    public OrderService(
        IOrderRepository orderRepository,
        IInventoryService inventoryService,
        IRegisterCustomerService registerCustomerService,
        IMercadoPagoPaymentAdapter mercadoPagoPaymentAdapter)
    {
        _orderRepository = orderRepository;
        _mercadoPagoPaymentAdapter = mercadoPagoPaymentAdapter;
        _registerCustomerService = registerCustomerService;
        _inventoryService = inventoryService;
    }

    public async Task<Pagination<GetResponse>> GetAllAsync(OrderFilter filter, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(filter.Status))
        {
            var page = await _orderRepository.GetAllPaginate(filter.Page, filter.Size, cancellationToken);
            return page.ToResponse();
        }

        var status = ParseOrderStatusFilter(filter);

        var filteredByStatusPage = await _orderRepository.GetAllByStatus(status, filter.Page, filter.Size, cancellationToken);
        return filteredByStatusPage.ToResponse();
    }

    public async Task<GetResponse> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(id, cancellationToken);

        OrderNotFoundException.ThrowIfNullOrEmpty(id, order);
        return order!.ToResponse();
    }


    public Task<string> CreateAsync(CreateRequest request, CancellationToken cancellationToken)
    {
        var items = request
            .Items
            .Select(item =>
            {
                _ = Enum.TryParse(item.Category, out ItemCategory category);

                return new OrderItem
                (
                    item.Id,
                    item.Name,
                    category,
                    item.Price,
                    item.Amount
                );
            });

        var order = new Order
        (
            request.CustomerId,
            request.CustomerName,
            items
        );

        var orderId = _orderRepository.CreateAsync(order, cancellationToken);

        return orderId;
    }
    public async Task<GetResponse> UpdateStatusAsync(string id, UpdateStatusRequest updateStatusRequest, CancellationToken cancellationToken)
    {
        InvalidOrderStatusException.ThrowIfNullOrEmpty(updateStatusRequest.Status);
        var orderStatus = ParseOrderStatus(updateStatusRequest.Status!);

        var order = await _orderRepository.UpdateStatusAsync(id, orderStatus, cancellationToken);

        if (orderStatus == OrderStatus.Finished)
        {
            var itemsQuantity = order.Items
                .Select(item => new ItemQuantity { ItemId = item.Id!, Quantity = item.Amount });

            _inventoryService.RegisterOrder(id, orderStatus.ToString(), DateTime.UtcNow, itemsQuantity);
        }

        return order.ToResponse();
    }
    public async Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        await _orderRepository.DeleteAsync(id, cancellationToken);
    }

    public async Task<CheckoutResponse> CheckoutAsync(string id, CheckoutRequest request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(id, cancellationToken);
        OrderNotFoundException.ThrowIfNullOrEmpty(id, order);

        PaymentMethodNotSupportedException.ThrowIfPaymentMethodIsNotSupported(request.PaymentType!);
        _ = Enum.TryParse(request.PaymentType, out PaymentMethod paymentMethod);

        var orderPaymentCheckout = await ProcessCheckoutAsync(order!, paymentMethod, cancellationToken);

        return new CheckoutResponse(
            orderPaymentCheckout.Id,
            orderPaymentCheckout.PaymentMethod,
            orderPaymentCheckout.QrCode,
            orderPaymentCheckout.QrCodeBase64,
            orderPaymentCheckout.Amount);
    }

    private async Task<OrderPaymentCheckoutEntity> ProcessCheckoutAsync(Order order, PaymentMethod paymentMethod, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(order?.CustomerId))
        {
            return await ExecuteCustomerCheckoutAsync(order!, paymentMethod, cancellationToken);
        }

        var orderPaymentCheckout = await ExecuteAnonymousCheckoutAsync(order!, paymentMethod, cancellationToken);
        return orderPaymentCheckout;
    }

    private async Task<OrderPaymentCheckoutEntity> ExecuteAnonymousCheckoutAsync(Order order, PaymentMethod paymentMethod, CancellationToken cancellationToken)
    {
        var checkoutInput = new CheckoutInput
        (
            "NoUser",
            "fakeUserName",
            "fakeemail@fake.com",
            order.Id!,
            order.TotalPrice
        );

        var orderPaymentCheckout = await _mercadoPagoPaymentAdapter.CreatePaymentAsync(checkoutInput!, paymentMethod, cancellationToken);
        return orderPaymentCheckout;
    }

    private async Task<OrderPaymentCheckoutEntity> ExecuteCustomerCheckoutAsync(Order order, PaymentMethod paymentMethod, CancellationToken cancellationToken)
    {
        var customer = await _registerCustomerService.GetByIdAsync(order!.CustomerId!, cancellationToken);

        if (customer is null)
        {
            throw new ArgumentNullException(nameof(customer), "Customer not found for the given order.");
        }

        var checkoutInput = new CheckoutInput
        (
            customer.CustomerIdentifier,
            order.CustomerName!,
            customer.Email,
            order.Id!,
            order.TotalPrice
        );

        var orderPaymentCheckout = await _mercadoPagoPaymentAdapter.CreatePaymentAsync(checkoutInput!, paymentMethod, cancellationToken);

        return orderPaymentCheckout;
    }

    public async Task ConfirmPaymentAsync(string id, CancellationToken cancellationToken)
    {
        await _orderRepository.UpdateStatusAsync(id, OrderStatus.Received, cancellationToken);
    }

    private static OrderStatus ParseOrderStatusFilter(OrderFilter filter)
    {
        if (!Enum.IsDefined(typeof(OrderStatus), filter.Status!))
        {
            throw new InvalidOrderStatusFilterException(filter.Status!);
        }

        return ParseOrderStatus(filter.Status!);
    }

    private static OrderStatus ParseOrderStatus(string text)
    {
        _ = Enum.TryParse(text, out OrderStatus status);

        return status;
    }
}
