using Business.UseCases.DTOs;
using Infrastructure.Entities;
using Infrastructure.MongoDb.Contexts.Interfaces;
using Infrastructure.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

internal class MenuItemMongoDbRepository : BaseRepository<MenuItemMongoDb>, IMenuItemMongoDbRepository
{
    public MenuItemMongoDbRepository(IMongoContext mongoContext) : base(mongoContext)
    {

    }

    public async Task<MenuItemMongoDb> InsertOneAsync(MenuItemMongoDb item, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(item, default, cancellationToken);

        return item;
    }

    public async Task<MenuItemMongoDb?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var filter = Builders<MenuItemMongoDb>
            .Filter
            .Eq(menuItem => menuItem.Id, id);

        var menuItemMongoDb = await _collection
            .Find(filter)
            .FirstOrDefaultAsync(cancellationToken);

        return menuItemMongoDb;
    }

    public async Task<IEnumerable<MenuItemMongoDb>> GetAllAsync(MenuItemFilter filter, CancellationToken cancellationToken)
    {
        var builder = Builders<MenuItemMongoDb>.Filter;

        var filters = new List<FilterDefinition<MenuItemMongoDb>>
        {
            builder.Eq(menuItem => menuItem.IsActive, true)
        };

        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            filters.Add(builder.Regex(menuItem => menuItem.Name, new BsonRegularExpression(filter.Name, "i")));
        }

        if (filter.Category is not null)
        {
            filters.Add(builder.Eq(menuItem => menuItem.Category, filter.Category.Value));
        }

        var finalFilter = builder.And(filters);

        var skip = filter.Skip > 0 ? filter.Skip : 0;
        var limit = filter.Limit > 0 ? filter.Limit : 0;

        var query = _collection
            .Find(finalFilter)
            .Skip(skip)
            .Limit(limit);

        var cursor = await query.ToCursorAsync(cancellationToken);

        return cursor.ToEnumerable(cancellationToken: cancellationToken);
    }

    public async Task<MenuItemMongoDb> UpdateAsync(string id, MenuItemMongoDb item, CancellationToken cancellationToken)
    {
        var filter = Builders<MenuItemMongoDb>
            .Filter
            .Eq(menuItem => menuItem.Id, id);

        var update = Builders<MenuItemMongoDb>.Update
            .Set(menuItem => menuItem.Name, item.Name)
            .Set(menuItem => menuItem.Price, item.Price)
            .Set(menuItem => menuItem.Category, item.Category)
            .Set(menuItem => menuItem.Description, item.Description)
            .Set(menuItem => menuItem.IsActive, item.IsActive);

        _ = await _collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);

        return item;
    }
}
