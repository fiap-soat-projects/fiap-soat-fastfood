using Business.Entities.Enums;
using Infrastructure.Entities;
using Infrastructure.Entities.Page;

namespace Infrastructure.Repositories.Interfaces;

internal interface IOrderMongoDbRepository
{
    Task<string> InsertOneAsync(OrderMongoDb order, CancellationToken cancellationToken);
    Task<OrderMongoDb?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<PagedResult<OrderMongoDb>> GetAllByStatus(OrderStatus status, int page, int size, CancellationToken cancellationToken);
    Task<PagedResult<OrderMongoDb>> GetAllPaginate(int page, int size, CancellationToken cancellationToken);
    Task DeleteAsync(string id, CancellationToken cancellationToken);
    Task<OrderMongoDb> UpdateStatusAsync(
        string id,
        OrderStatus status,
        CancellationToken cancellationToken);
    Task<OrderMongoDb> UpdatePaymentMethodAsync(
        string id,
        PaymentMethod paymentMethod,
        CancellationToken cancellationToken);
}
