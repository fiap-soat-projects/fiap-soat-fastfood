using Application.DTOs;
using Application.DTOs.Extensions;
using Application.UseCases.Interfaces;
using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Services.Exceptions;
using Domain.Services.Interfaces;

namespace Application.UseCases;
internal class OrderUseCase : IOrderUseCase
{
    private readonly IOrderService _orderService;
    private readonly IInventoryService _inventoryService;
    private readonly IPaymentService _paymentService;

    public OrderUseCase(
        IOrderService orderService,
        IInventoryService inventoryService,
        IPaymentService paymentService)
    {
        _orderService = orderService;
        _inventoryService = inventoryService;
        _paymentService = paymentService;
    }

    public async Task<Pagination<OrderGetResponse>> GetAllAsync(OrderFilter filter, CancellationToken cancellationToken)
    {
        var status = ParseOrderStatusFilter(filter);

        var orderPage = await _orderService.GetAllAsync(cancellationToken, status, filter.Page, filter.Size);
        return orderPage.ToResponse();
    }

    public async Task<OrderGetResponse> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var order = await _orderService.GetByIdAsync(id, cancellationToken);

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

        var orderId = _orderService.CreateAsync(order, cancellationToken);

        return orderId;
    }

    public async Task<OrderGetResponse> UpdateStatusAsync(string id, UpdateStatusRequest updateStatusRequest, CancellationToken cancellationToken)
    {
        InvalidOrderStatusException.ThrowIfNullOrEmpty(updateStatusRequest.Status);
        var orderStatus = ParseOrderStatus(updateStatusRequest.Status!);

        var order = await _orderService.UpdateStatusAsync(id, orderStatus, cancellationToken);

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
        await _orderService.DeleteAsync(id, cancellationToken);
    }

    public async Task<CheckoutResponse> CheckoutAsync(string id, CheckoutRequest request, CancellationToken cancellationToken)
    {
        var order = await _orderService.GetByIdAsync(id, cancellationToken);
        OrderNotFoundException.ThrowIfNullOrEmpty(id, order);

        PaymentMethodNotSupportedException.ThrowIfPaymentMethodIsNotSupported(request.PaymentType!);
        _ = Enum.TryParse(request.PaymentType, out PaymentMethod paymentMethod);

        var orderPaymentCheckout = await _paymentService.CheckoutAsync(order!, paymentMethod, cancellationToken);

        return new CheckoutResponse(
            orderPaymentCheckout.Id,
            orderPaymentCheckout.PaymentMethod,
            orderPaymentCheckout.QrCode,
            orderPaymentCheckout.QrCodeBase64,
            orderPaymentCheckout.Amount);
    }

    public async Task ConfirmPaymentAsync(string id, CancellationToken cancellationToken)
    {
        await _paymentService.ConfirmPaymentAsync(id, cancellationToken);
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
