using Business.Entities;
using Business.Exceptions;
using Business.Gateways.Repositories.Interfaces;
using Business.UseCases.DTOs;
using Infrastructure.Entities;
using Infrastructure.Repositories.Interfaces;
using MongoDB.Driver;

namespace Adapter.Gateways.Repositories;

internal class MenuItemGateway : IMenuItemRepository
{
    private readonly IMenuItemMongoDbRepository _menuItemMongoDbRepository;

    public MenuItemGateway(IMenuItemMongoDbRepository menuItemMongoDbRepository)
    {
        _menuItemMongoDbRepository = menuItemMongoDbRepository;
    }

    public async Task<MenuItem> CreateAsync(MenuItem menuItem, CancellationToken cancellationToken)
    {
        try
        {
            var menuItemMongoDb = new MenuItemMongoDb(menuItem);

            menuItemMongoDb = await _menuItemMongoDbRepository.InsertOneAsync(menuItemMongoDb, cancellationToken);

            return menuItemMongoDb.ToDomain();
        }
        catch (MongoWriteException exception) when (exception.WriteError.Category is ServerErrorCategory.DuplicateKey)
        {
            throw new DuplicatedItemException<MenuItem>(nameof(MenuItem.Name));
        }
    }

    public async Task<MenuItem?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var menuItemMongoDb = await _menuItemMongoDbRepository.GetByIdAsync(id, cancellationToken);

        if (menuItemMongoDb is null)
        {
            return default;
        }

        return menuItemMongoDb.ToDomain();
    }

    public async Task<IEnumerable<MenuItem>> GetAllAsync(MenuItemFilter filter, CancellationToken cancellationToken)
    {
        var menuItemsMongoDb = await _menuItemMongoDbRepository.GetAllAsync(filter, cancellationToken);

        var menuItems = menuItemsMongoDb?.Select(menuItem => menuItem.ToDomain()) ?? [];

        return menuItems;
    }

    public async Task<MenuItem> UpdateAsync(string id, MenuItem menuItem, CancellationToken cancellationToken)
    {
        var menuItemMongoDb = new MenuItemMongoDb(menuItem);

        menuItemMongoDb = await _menuItemMongoDbRepository.UpdateAsync(id, menuItemMongoDb, cancellationToken);

        return menuItemMongoDb.ToDomain();
    }
}
