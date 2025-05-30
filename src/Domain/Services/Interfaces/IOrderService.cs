using Domain.Entities;
using Domain.Entities.Enums;

namespace Domain.Services.Interfaces;
internal interface IOrderService
{
    Task<Order> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<string> CreateAsync(Order order, CancellationToken cancellationToken);
    Task DeleteAsync(string id, CancellationToken cancellationToken);
    Task<Order> UpdateStatusAsync(string id, OrderStatus status, CancellationToken cancellationToken);
    Task<Pagination<Order>> GetAllAsync(
        CancellationToken cancellationToken,
        OrderStatus? Status = null,
        int Page = 0,
        int Size = 0);
}
