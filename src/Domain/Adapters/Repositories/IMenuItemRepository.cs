using Domain.Entities;
using Domain.Services.DTOs;

namespace Domain.Adapters.Repositories;

internal interface IMenuItemRepository
{
    Task<MenuItem> CreateAsync(MenuItem menuItem, CancellationToken cancellationToken);
    Task<MenuItem?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<IEnumerable<MenuItem>> GetAllAsync(MenuItemFilter filter, CancellationToken cancellationToken);
    Task<MenuItem> UpdateAsync(string id, MenuItem menuItem, CancellationToken cancellationToken);
}