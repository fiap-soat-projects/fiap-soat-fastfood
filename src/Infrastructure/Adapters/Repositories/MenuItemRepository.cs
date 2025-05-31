using Domain.Adapters.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Services.DTOs;
using Infrastructure.Entities;
using Infrastructure.Repositories.Interfaces;
using MongoDB.Driver;

namespace Infrastructure.Adapters.Repositories;

internal class MenuItemRepository : IMenuItemRepository
{
    private readonly IMenuItemMongoDbRepository _repository;

    public MenuItemRepository(IMenuItemMongoDbRepository repository)
    {
        _repository = repository;
    }

    public async Task<MenuItem> CreateAsync(MenuItem menuItem, CancellationToken cancellationToken)
    {
        try
        {
            var menuItemMongoDb = new MenuItemMongoDb(menuItem);

            menuItemMongoDb = await _repository.InsertOneAsync(menuItemMongoDb, cancellationToken);

            return menuItemMongoDb.ToDomain();
        }
        catch (MongoWriteException exception) when (exception.WriteError.Category is ServerErrorCategory.DuplicateKey)
        {
            throw new DuplicatedItemException<MenuItem>(nameof(MenuItem.Name));
        }
    }

    public Task<MenuItem?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<MenuItem>> GetAllAsync(MenuItemFilter filter, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(string id, MenuItem menuItem, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
