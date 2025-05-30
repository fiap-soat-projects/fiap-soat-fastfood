using Domain.Entities;
using Domain.Entities.Enums;

namespace Domain.Repositories.Interfaces;
internal interface IOrderRepository
{
    Task<string> CreateAsync(Order order, CancellationToken cancellationToken);
    Task<Order?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<Pagination<Order>> GetAllByStatus(OrderStatus status, int page, int size, CancellationToken cancellationToken);
    Task<Pagination<Order>> GetAllPaginate(int page, int size, CancellationToken cancellationToken);
    Task DeleteAsync(string id, CancellationToken cancellationToken);
    Task<Order> UpdateStatusAsync(
        string id,
        OrderStatus status,
        CancellationToken cancellationToken);
}
