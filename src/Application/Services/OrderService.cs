using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Repositories.Interfaces;
using Domain.Services.Exceptions;
using Domain.Services.Interfaces;

namespace Application.Services;
internal class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public Task<string> CreateAsync(Order order, CancellationToken cancellationToken)
    {
        var orderId = _orderRepository.CreateAsync(order, cancellationToken);
        return orderId;
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        await _orderRepository.DeleteAsync(id, cancellationToken);
    }

    public async Task<Pagination<Order>> GetAllAsync(CancellationToken cancellationToken, OrderStatus? status = null, int page = 0, int size = 0)
    {
        if (status is null)
        {
            var pageWithoutFilters = await _orderRepository.GetAllPaginate(page, size, cancellationToken);
            return pageWithoutFilters;
        }

        var pageWithStatusFilter = await _orderRepository.GetAllByStatus(status.Value, page, size, cancellationToken);
        return pageWithStatusFilter;
    }

    public async Task<Order> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(id, cancellationToken);

        OrderNotFoundException.ThrowIfNullOrEmpty(id, order);
        return order!;
    }

    public async Task<Order> UpdateStatusAsync(string id, OrderStatus status, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.UpdateStatusAsync(id, status, cancellationToken);
        return order;
    }
}
