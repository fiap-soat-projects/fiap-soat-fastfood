using Domain.Entities;
using Domain.Services.DTOs;

namespace Domain.Services.Interfaces;

public interface IMenuItemService
{
    Task<MenuItem> CreateAsync(MenuItem item, CancellationToken cancellationToken);
    Task<MenuItem> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<IEnumerable<MenuItem>> GetAllAsync(MenuItemFilter filter, CancellationToken cancellationToken);
    Task<MenuItem> UpdateAsync(MenuItem item, CancellationToken cancellationToken);
    Task SoftDeleteAsync(string id, CancellationToken cancellationToken);
}