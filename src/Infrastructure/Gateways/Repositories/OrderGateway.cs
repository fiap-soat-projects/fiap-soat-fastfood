using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Entities.Page;
using Domain.Gateways.Interfaces;
using Infrastructure.Entities;
using Infrastructure.Entities.Extensions;
using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.Gateways.Repositories;
internal class OrderGateway : IOrderGateway
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
        var pagedResult = await _orderMongoDbRepository.GetAllByStatus(status, page, size, cancellationToken);

        var pagedDomain = pagedResult.ToDomain();

        return pagedDomain;
    }

    public async Task<Pagination<Order>> GetAllPaginateAsync(int page, int size, CancellationToken cancellationToken)
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

    public async Task<Order> UpdatePaymentMethodAsync(string id, PaymentMethod paymentMethod, CancellationToken cancellationToken)
    {
        var orderMongoDb = await _orderMongoDbRepository.UpdatePaymentMethodAsync(id, paymentMethod, cancellationToken);

        return orderMongoDb.ToDomain();
    }
}
