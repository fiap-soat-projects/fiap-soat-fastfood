using Business.Entities;
using Business.Exceptions;
using Business.Gateways.Repositories.Interfaces;
using Business.UseCases.DTOs;
using Business.UseCases.Interfaces;

namespace Business.UseCases;

internal class MenuItemUseCase : IMenuItemUseCase
{
    private readonly IMenuItemRepository _menuItemRepository;

    public MenuItemUseCase(IMenuItemRepository menuItemGateway)
    {
        _menuItemRepository = menuItemGateway;
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

    public async Task<MenuItem> UpdateAsync(MenuItem item, CancellationToken cancellationToken)
    {
        _ = await GetByIdAsync(item.Id, cancellationToken);

        return await _menuItemRepository.UpdateAsync(item.Id, item, cancellationToken);
    }

    public async Task SoftDeleteAsync(string id, CancellationToken cancellationToken)
    {
        var item = await GetByIdAsync(id, cancellationToken);

        item.SetInactive();

        await UpdateAsync(item, cancellationToken);
    }
}
