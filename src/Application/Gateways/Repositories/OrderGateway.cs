using Business.Entities;
using Business.Entities.Enums;
using Business.Entities.Page;
using Business.Gateways.Repositories.Interfaces;
using Infrastructure.Entities;
using Infrastructure.Entities.Extensions;
using Infrastructure.Repositories.Interfaces;

namespace Adapter.Gateways.Repositories;
internal class OrderGateway : IOrderRepository
{
    private readonly IOrderMongoDbRepository _orderMongoDbRepository;

    public OrderGateway(IOrderMongoDbRepository orderMongoDbRepository)
    {
        _orderMongoDbRepository = orderMongoDbRepository;
    }

    public Task<string> CreateAsync(Order order, CancellationToken cancellationToken)
    {
        var orderMongoDb = new OrderMongoDb(order);

        return _orderMongoDbRepository.InsertOneAsync(orderMongoDb, cancellationToken);
    }

    public Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        return _orderMongoDbRepository.DeleteAsync(id, cancellationToken);
    }

    public async Task<Pagination<Order>> GetAllByStatusAsync(OrderStatus status, int page, int size, CancellationToken cancellationToken)
    {
        var pagedResult = await _orderMongoDbRepository.GetAllByStatusAsync(status, page, size, cancellationToken);

        var pagedDomain = pagedResult.ToDomain();

        return pagedDomain;
    }

    public async Task<Pagination<Order>> GetAllPaginateAsync(int page, int size, CancellationToken cancellationToken)
    {
        var pagedResult = await _orderMongoDbRepository.GetAllPaginateAsync(page, size, cancellationToken);

        var pagedDomain = pagedResult.ToDomain();

        return pagedDomain;
    }

    public async Task<Pagination<Order>> GetActivePaginateAsync(int page, int size, CancellationToken cancellationToken)
    {
        var pagedResult = await _orderMongoDbRepository.GetActivePaginateAsync(page, size, cancellationToken);

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

    public async Task UpdatePaymentAsync(string id, OrderStatus orderStatus, Payment payment, CancellationToken cancellationToken)
    {
        var paymentMongoDb = new PaymentMongoDb(payment);   

        await _orderMongoDbRepository.UpdatePaymentAsync(id, orderStatus, paymentMongoDb, cancellationToken);
    }
}
