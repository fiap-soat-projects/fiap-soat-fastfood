using Business.Entities;
using Business.Entities.Enums;
using Business.Entities.Page;

namespace Business.Gateways.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<string> CreateAsync(Order order, CancellationToken cancellationToken);
    Task<Order?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<Pagination<Order>> GetAllByStatusAsync(OrderStatus status, int page, int size, CancellationToken cancellationToken);
    Task<Pagination<Order>> GetAllPaginateAsync(int page, int size, CancellationToken cancellationToken);
    Task<Pagination<Order>> GetActivePaginateAsync(int page, int size, CancellationToken cancellationToken);
    Task DeleteAsync(string id, CancellationToken cancellationToken);

    Task<Order> UpdateStatusAsync(
        string id,
        OrderStatus status,
        CancellationToken cancellationToken);

    Task UpdatePaymentAsync(
        string id,
        OrderStatus status,
        Payment payment,
        CancellationToken cancellationToken);
}
