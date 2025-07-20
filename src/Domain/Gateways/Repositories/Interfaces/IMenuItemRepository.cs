using Business.Entities;
using Business.UseCases.DTOs;

namespace Business.Gateways.Repositories.Interfaces;

internal interface IMenuItemRepository
{
    Task<MenuItem> CreateAsync(MenuItem menuItem, CancellationToken cancellationToken);
    Task<MenuItem?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<IEnumerable<MenuItem>> GetAllAsync(MenuItemFilter filter, CancellationToken cancellationToken);
    Task<MenuItem> UpdateAsync(string id, MenuItem menuItem, CancellationToken cancellationToken);
}