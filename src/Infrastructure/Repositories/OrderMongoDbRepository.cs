using Business.Entities.Enums;
using Infrastructure.Entities;
using Infrastructure.Entities.Page;
using Infrastructure.MongoDb.Contexts.Interfaces;
using Infrastructure.Repositories.Interfaces;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;
internal class OrderMongoDbRepository : BaseRepository<OrderMongoDb>, IOrderMongoDbRepository
{
    public OrderMongoDbRepository(IMongoContext mongoContext) : base(mongoContext)
    {

    }

    public async Task<string> InsertOneAsync(OrderMongoDb order, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(order, null, cancellationToken);

        return order.Id;
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        var filter = Builders<OrderMongoDb>.Filter.Eq(entity => entity.Id, id);

        await _collection.DeleteOneAsync(filter, cancellationToken);
    }

    public async Task<PagedResult<OrderMongoDb>> GetAllByStatusAsync(OrderStatus status, int page, int size, CancellationToken cancellationToken)
    {
        var filter = Builders<OrderMongoDb>.Filter.Eq(entity => entity.Status, status);

        var pagedResult = await GetPagedAsync(page, size, filter, cancellationToken: cancellationToken);

        return pagedResult;
    }

    public async Task<PagedResult<OrderMongoDb>> GetAllPaginateAsync(int page, int size, CancellationToken cancellationToken)
    {
        var filter = Builders<OrderMongoDb>.Filter.Empty;

        var pagedResult = await GetPagedAsync(page, size, filter, cancellationToken: cancellationToken);

        return pagedResult;
    }

    public async Task<PagedResult<OrderMongoDb>> GetActivePaginateAsync(int page, int size, CancellationToken cancellationToken)
    {
        var filter = Builders<OrderMongoDb>.Filter.Nin(x => x.Status, [OrderStatus.Finished, OrderStatus.Canceled, OrderStatus.None]);

        var sort = Builders<OrderMongoDb>.Sort.Combine(
            Builders<OrderMongoDb>.Sort.Ascending(x => x.Status),
            Builders<OrderMongoDb>.Sort.Ascending(x => x.CreatedAt));

        var pagedResult = await GetPagedAsync(page, size, filter, sort, cancellationToken);     
        
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

    public async Task UpdatePaymentAsync(string id, OrderStatus orderStatus, PaymentMongoDb payment, CancellationToken cancellationToken)
    {
        var filter = Builders<OrderMongoDb>.Filter.Eq(entity => entity.Id, id);

        var update = Builders<OrderMongoDb>.Update
            .Set(entity => entity.Payment, payment)
            .Set(entity => entity.Status, orderStatus);


        var options = new FindOneAndUpdateOptions<OrderMongoDb>
        {
            ReturnDocument = ReturnDocument.After
        };

        _ =  await _collection.FindOneAndUpdateAsync(filter, update, options, cancellationToken: cancellationToken);
    }
}
