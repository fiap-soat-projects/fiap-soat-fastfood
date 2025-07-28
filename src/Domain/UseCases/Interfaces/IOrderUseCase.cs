using Business.Entities;
using Business.Entities.Enums;
using Business.Entities.Page;

namespace Business.UseCases.Interfaces;

internal interface IOrderUseCase
{
    Task<Order> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<string> CreateAsync(Order order, CancellationToken cancellationToken);
    Task DeleteAsync(string id, CancellationToken cancellationToken);
    Task<Order> UpdateStatusAsync(string id, OrderStatus status, CancellationToken cancellationToken);
    Task<Pagination<Order>> GetAllAsync(CancellationToken cancellationToken, OrderStatus? status = null, int page = 0, int size = 0);
    Task<Pagination<Order>> GetActiveAsync(CancellationToken cancellationToken, int page = 0, int size = 0);
}
