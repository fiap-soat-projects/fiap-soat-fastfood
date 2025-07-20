using Adapter.Controllers.DTOs;
using Adapter.Controllers.DTOs.Extensions;
using Adapter.Controllers.DTOs.Filters;
using Adapter.Controllers.Interfaces;
using Adapter.Presenters;
using Business.Entities;
using Business.Entities.Enums;
using Business.Entities.Page;
using Business.Exceptions;
using Business.UseCases.Exceptions;
using Business.UseCases.Interfaces;

namespace Adapter.Controllers;
internal class OrderController : IOrderController
{
    private readonly IOrderUseCase _orderUseCase;
    private readonly IMenuItemUseCase _menuItemUseCase;
    private readonly IInventoryUseCase _inventoryUseCase;
    private readonly ITransactionUseCase _paymentUseCase;

    public OrderController(
        IOrderUseCase orderUseCase,
        IMenuItemUseCase menuItemUseCase,
        IInventoryUseCase inventoryUseCase,
        ITransactionUseCase paymentUseCase)
    {
        _orderUseCase = orderUseCase;
        _menuItemUseCase = menuItemUseCase;
        _inventoryUseCase = inventoryUseCase;
        _paymentUseCase = paymentUseCase;
    }

    public async Task<Pagination<OrderGetResponse>> GetAllAsync(OrderFilter filter, CancellationToken cancellationToken)
    {
        var status = ParseOrderStatus(filter.Status!);

        var orderPage = await _orderUseCase.GetAllAsync(cancellationToken, status, filter.Page, filter.Size);

        return orderPage.ToResponse();
    }

    public async Task<OrderGetResponse> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var order = await _orderUseCase.GetByIdAsync(id, cancellationToken);

        OrderNotFoundException.ThrowIfNull(order, id);

        return order!.ToResponse();
    }

    public async Task<string> CreateAsync(CreateRequest request, CancellationToken cancellationToken)
    {
        var orderItems = new List<OrderItem>();

        foreach (var item in request.Items)
        {
            var menuItem = await _menuItemUseCase.GetByIdAsync(item.Id!, cancellationToken);

            orderItems.Add(new OrderItem
            (
                menuItem.Id!,
                menuItem.Name!,
                menuItem.Category,
                menuItem.Price,
                item.Amount
            ));
        }

        var order = new Order
        (
            request.CustomerId,
            request.CustomerName,
            orderItems
        );

        var orderId = await _orderUseCase.CreateAsync(order, cancellationToken);

        return orderId;
    }

    public async Task<OrderGetResponse> UpdateStatusAsync(string id, UpdateStatusRequest updateStatusRequest, CancellationToken cancellationToken)
    {
        InvalidOrderStatusException.ThrowIfNullOrEmpty(updateStatusRequest.Status);

        var orderStatus = ParseOrderStatus(updateStatusRequest.Status!);

        var order = await _orderUseCase.UpdateStatusAsync(id, orderStatus, cancellationToken);

        if (orderStatus == OrderStatus.Finished)
        {
            _inventoryUseCase.GenerateAuditLog(order, DateTime.UtcNow);
        }

        return order.ToResponse();
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        await _orderUseCase.DeleteAsync(id, cancellationToken);
    }

    public async Task<CheckoutResponse> CheckoutAsync(string id, CheckoutRequest request, CancellationToken cancellationToken)
    {
        var order = await _orderUseCase.GetByIdAsync(id, cancellationToken);

        PaymentMethodNotSupportedException.ThrowIfPaymentMethodIsNotSupported(request.PaymentType!);

        _ = Enum.TryParse(request.PaymentType, out PaymentMethod paymentMethod);

        var orderPaymentCheckout = await _paymentUseCase.CheckoutAsync(order!, paymentMethod, cancellationToken);

        return new CheckoutResponse(
            orderPaymentCheckout.Id,
            orderPaymentCheckout.PaymentMethod,
            orderPaymentCheckout.QrCode,
            orderPaymentCheckout.QrCodeBase64,
            orderPaymentCheckout.Amount);
    }

    public async Task ConfirmPaymentAsync(string id, CancellationToken cancellationToken)
    {
        await _paymentUseCase.ConfirmPaymentAsync(id, cancellationToken);
    }

    private static OrderStatus ParseOrderStatus(string text)
    {
        _ = Enum.TryParse(text, out OrderStatus status);

        return status;
    }
}
