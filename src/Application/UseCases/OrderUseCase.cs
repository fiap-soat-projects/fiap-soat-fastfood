using Application.Exceptions;
using Application.UseCases.DTOs.Extensions;
using Application.UseCases.DTOs.Filters;
using Application.UseCases.DTOs.Request;
using Application.UseCases.DTOs.Response;
using Application.UseCases.Interfaces;
using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Entities.Page;
using Domain.Services.Exceptions;
using Domain.Services.Interfaces;

namespace Application.UseCases;
internal class OrderUseCase : IOrderUseCase
{
    private readonly IOrderService _orderService;
    private readonly IMenuItemService _menuItemService;
    private readonly IInventoryService _inventoryService;
    private readonly ITransactionService _paymentService;

    public OrderUseCase(
        IOrderService orderService,
        IMenuItemService menuItemService,
        IInventoryService inventoryService,
        ITransactionService paymentService)
    {
        _orderService = orderService;
        _menuItemService = menuItemService;
        _inventoryService = inventoryService;
        _paymentService = paymentService;
    }

    public async Task<Pagination<OrderGetResponse>> GetAllAsync(OrderFilter filter, CancellationToken cancellationToken)
    {
        var status = ParseOrderStatus(filter.Status!);

        var orderPage = await _orderService.GetAllAsync(cancellationToken, status, filter.Page, filter.Size);

        return orderPage.ToResponse();
    }

    public async Task<OrderGetResponse> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var order = await _orderService.GetByIdAsync(id, cancellationToken);

        OrderNotFoundException.ThrowIfNull(order, id);

        return order!.ToResponse();
    }

    public async Task<string> CreateAsync(CreateRequest request, CancellationToken cancellationToken)
    {
        var orderItems = new List<OrderItem>();

        foreach (var item in request.Items)
        {
            var menuItem = await _menuItemService.GetByIdAsync(item.Id!, cancellationToken);

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

        var orderId = await _orderService.CreateAsync(order, cancellationToken);

        return orderId;
    }

    public async Task<OrderGetResponse> UpdateStatusAsync(string id, UpdateStatusRequest updateStatusRequest, CancellationToken cancellationToken)
    {
        InvalidOrderStatusException.ThrowIfNullOrEmpty(updateStatusRequest.Status);

        var orderStatus = ParseOrderStatus(updateStatusRequest.Status!);

        var order = await _orderService.UpdateStatusAsync(id, orderStatus, cancellationToken);

        if (orderStatus == OrderStatus.Finished)
        {
            _inventoryService.GenerateAuditLog(order, DateTime.UtcNow);
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

    private static OrderStatus ParseOrderStatus(string text)
    {
        _ = Enum.TryParse(text, out OrderStatus status);

        return status;
    }
}
