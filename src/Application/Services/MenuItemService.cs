using Application.Exceptions;
using Domain.Adapters.Repositories;
using Domain.Entities;
using Domain.Services.DTOs;
using Domain.Services.Interfaces;

namespace Application.Services;

internal class MenuItemService : IMenuItemService
{
    private readonly IMenuItemRepository _menuItemRepository;

    public MenuItemService(IMenuItemRepository menuItemRepository)
    {
        _menuItemRepository = menuItemRepository;
    }

    public async Task<MenuItem> CreateAsync(MenuItem item, CancellationToken cancellationToken)
    {
        item = await _menuItemRepository.CreateAsync(item, cancellationToken);

        return item;
    }

    public async Task<MenuItem> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrEmpty(id, nameof(id));

        var item = await _menuItemRepository.GetByIdAsync(id, cancellationToken);

        MenuItemNotFoundException.ThrowIfNull(item, id);

        return item!;
    }

    public async Task<IEnumerable<MenuItem>> GetAllAsync(MenuItemFilter filter, CancellationToken cancellationToken)
    {
        var items = await _menuItemRepository.GetAllAsync(filter, cancellationToken);

        return items;
    }

    public async Task UpdateAsync(MenuItem item, CancellationToken cancellationToken)
    {
        _ = await GetByIdAsync(item.Id, cancellationToken);

        await _menuItemRepository.UpdateAsync(item.Id, item, cancellationToken);
    }

    public async Task SoftDeleteAsync(string id, CancellationToken cancellationToken)
    {
        var item = await GetByIdAsync(id, cancellationToken);

        item.SetInactive();

        await UpdateAsync(item, cancellationToken);
    }
}
