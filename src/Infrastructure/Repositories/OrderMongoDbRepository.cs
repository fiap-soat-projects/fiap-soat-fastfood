using Domain.Entities.Enums;
using Infrastructure.Contexts.Interfaces;
using Infrastructure.Entities;
using Infrastructure.Repositories.Entities;
using Infrastructure.Repositories.Interfaces;
using MongoDB.Driver;

namespace Infrastructure.Repositories;
internal class OrderMongoDbRepository : BaseRepository<OrderMongoDb>, IOrderMongoDbRepository
{
    public OrderMongoDbRepository(IMongoContext mongoContext) : base(mongoContext)
    {
    }

    public async Task<string> CreateAsync(OrderMongoDb order, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(order, null, cancellationToken);
        return order.Id;
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        var filter = Builders<OrderMongoDb>.Filter.Eq(entity => entity.Id, id);

        await _collection.DeleteOneAsync(filter, cancellationToken);
    }

    public async Task<PagedResult<OrderMongoDb>> GetAllByStatus(OrderStatus status, int page, int size, CancellationToken cancellationToken)
    {
        var filter = Builders<OrderMongoDb>.Filter.Eq(entity => entity.Status, status);

        var pagedResult = await GetPagedAsync(page, size, filter, cancellationToken);

        return pagedResult;
    }

    public async Task<PagedResult<OrderMongoDb>> GetAllPaginate(int page, int size, CancellationToken cancellationToken)
    {

        var filter = Builders<OrderMongoDb>.Filter.Empty;
        var options = new FindOptions<OrderMongoDb>
        {
            Skip = (page - 1) * size,
            Limit = size
        };

        var pagedResult = await GetPagedAsync(page, size, filter, cancellationToken);

        return pagedResult;
    }

    public async Task<OrderMongoDb?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var filter = Builders<OrderMongoDb>.Filter.Eq(entity => entity.Id, id);
        var order = await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);

        return order;
    }

    public Task<OrderMongoDb> UpdateStatusAsync(string id, OrderStatus status, CancellationToken cancellationToken)
    {
        var filter = Builders<OrderMongoDb>.Filter.Eq(entity => entity.Id, id);
        var update = Builders<OrderMongoDb>.Update.Set(entity => entity.Status, status);

        var options = new FindOneAndUpdateOptions<OrderMongoDb>
        {
            ReturnDocument = ReturnDocument.After
        };

        var order = _collection.FindOneAndUpdateAsync(filter, update, options, cancellationToken: cancellationToken);

        return order;
    }
}
