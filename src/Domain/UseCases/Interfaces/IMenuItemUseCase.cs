using Business.Entities;
using Business.UseCases.DTOs;

namespace Business.UseCases.Interfaces;

internal interface IMenuItemUseCase
{
    Task<MenuItem> CreateAsync(MenuItem item, CancellationToken cancellationToken);
    Task<MenuItem> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<IEnumerable<MenuItem>> GetAllAsync(MenuItemFilter filter, CancellationToken cancellationToken);
    Task<MenuItem> UpdateAsync(MenuItem item, CancellationToken cancellationToken);
    Task SoftDeleteAsync(string id, CancellationToken cancellationToken);
}