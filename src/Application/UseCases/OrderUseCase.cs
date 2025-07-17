using Application.Exceptions;
using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Entities.Page;
using Domain.Gateways.Interfaces;
using Domain.UseCases.Interfaces;

namespace Application.UseCases;

internal class OrderUseCase : IOrderUseCase
{
    private readonly IOrderGateway _orderGateway;

    public OrderUseCase(IOrderGateway orderRepository)
    {
        _orderGateway = orderRepository;
    }

    public Task<string> CreateAsync(Order order, CancellationToken cancellationToken)
    {
        var orderId = _orderGateway.CreateAsync(order, cancellationToken);

        return orderId;
    }

    public async Task<Order> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var order = await _orderGateway.GetByIdAsync(id, cancellationToken);

        OrderNotFoundException.ThrowIfNull(order, id);

        return order!;
    }

    public async Task<Pagination<Order>> GetAllAsync(CancellationToken cancellationToken, OrderStatus? status = null, int page = 0, int size = 0)
    {
        if (status is null)
        {
            var pageWithoutFilters = await _orderGateway.GetAllPaginateAsync(page, size, cancellationToken);

            return pageWithoutFilters;
        }

        var pageWithStatusFilter = await _orderGateway.GetAllByStatusAsync(status.Value, page, size, cancellationToken);

        return pageWithStatusFilter;
    }

    public async Task<Order> UpdateStatusAsync(string id, OrderStatus status, CancellationToken cancellationToken)
    {
        var order = await _orderGateway.UpdateStatusAsync(id, status, cancellationToken);

        return order;
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        await _orderGateway.DeleteAsync(id, cancellationToken);
    }
}
