using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Repositories.Interfaces;
using Infrastructure.Repositories.Entities;
using Infrastructure.Repositories.Entities.Extensions;
using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.Repositories.Adapters;
internal class OrderRepository : IOrderRepository
{
    private readonly IOrderMongoDbRepository _orderMongoDbRepository;

    public OrderRepository(IOrderMongoDbRepository orderMongoDbRepository)
    {
        _orderMongoDbRepository = orderMongoDbRepository;
    }

    public Task<string> CreateAsync(Order order, CancellationToken cancellationToken)
    {
        var orderMongoDb = new OrderMongoDb(order);
        return _orderMongoDbRepository.CreateAsync(orderMongoDb, cancellationToken);
    }

    public Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        return _orderMongoDbRepository.DeleteAsync(id, cancellationToken);
    }

    public async Task<Pagination<Order>> GetAllByStatus(OrderStatus status, int page, int size, CancellationToken cancellationToken)
    {
        var pagedResult = await _orderMongoDbRepository.GetAllByStatus(status, page, size, cancellationToken);

        var pagedDomain = pagedResult.ToDomain();
        return pagedDomain;
    }

    public async Task<Pagination<Order>> GetAllPaginate(int page, int size, CancellationToken cancellationToken)
    {
        var pagedResult = await _orderMongoDbRepository.GetAllPaginate(page, size, cancellationToken);

        var pagedDomain = pagedResult.ToDomain();
        return pagedDomain;
    }

    public async Task<Order?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var orderMongoDB = await _orderMongoDbRepository.GetByIdAsync(id, cancellationToken);
        return orderMongoDB?.ToDomain();
    }

    public async Task<Order> UpdateStatusAsync(string id, OrderStatus status, CancellationToken cancellationToken)
    {
        var orderMongoDb = await _orderMongoDbRepository.UpdateStatusAsync(id, status, cancellationToken);
        return orderMongoDb.ToDomain();
    }
}
