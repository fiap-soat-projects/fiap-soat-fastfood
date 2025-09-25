using Business.Entities;
using Business.Entities.Enums;
using Business.Entities.Page;
using Business.Exceptions;
using Business.Gateways.Repositories.Interfaces;
using Business.UseCases.Interfaces;

namespace Business.UseCases;

internal class OrderUseCase : IOrderUseCase
{
    private readonly IOrderRepository _orderRepository;

    public OrderUseCase(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public Task<string> CreateAsync(Order order, CancellationToken cancellationToken)
    {
        var orderId = _orderRepository.CreateAsync(order, cancellationToken);

        return orderId;
    }

    public async Task<Order> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(id, cancellationToken);

        OrderNotFoundException.ThrowIfNull(order, id);

        return order!;
    }

    public async Task<Pagination<Order>> GetAllAsync(CancellationToken cancellationToken, OrderStatus? status = null, int page = 0, int size = 0)
    {
        if (status is null || status == OrderStatus.None)
        {
            var pageWithoutFilters = await _orderRepository.GetAllPaginateAsync(page, size, cancellationToken);

            return pageWithoutFilters;
        }

        var pageWithStatusFilter = await _orderRepository.GetAllByStatusAsync(status.Value, page, size, cancellationToken);

        return pageWithStatusFilter;
    }

    public async Task<Pagination<Order>> GetActiveAsync(CancellationToken cancellationToken, int page = 0, int size = 0)
    {
        var pageWithoutFilters = await _orderRepository.GetActivePaginateAsync(page, size, cancellationToken);

        return pageWithoutFilters;    
    }

    public async Task<Order> UpdateStatusAsync(string id, OrderStatus status, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.UpdateStatusAsync(id, status, cancellationToken);

        return order;
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        await _orderRepository.DeleteAsync(id, cancellationToken);
    }
}
