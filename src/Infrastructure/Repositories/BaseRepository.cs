using Infrastructure.Entities.Interfaces;
using Infrastructure.Entities.Page;
using Infrastructure.MongoDb.Contexts.Interfaces;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Repositories;

[ExcludeFromCodeCoverage]
public abstract class BaseRepository<TEntity> where TEntity : IMongoEntity
{
    protected readonly IMongoCollection<TEntity> _collection;

    protected BaseRepository(IMongoContext context)
    {
        _collection = context.GetCollection<TEntity>();
    }

    protected async Task<PagedResult<TEntity>> GetPagedAsync(
        int page,
        int size,
        FilterDefinition<TEntity> filter,   
        SortDefinition<TEntity>? sort = null,
        CancellationToken cancellationToken = default)
    {
        var options = new FindOptions<TEntity>
        {
            Skip = (page - 1) * size,
            Limit = size,
            Sort = sort
        };

        var cursor = await _collection.FindAsync(
            filter,
            options,
            cancellationToken);

        var orders = cursor.ToEnumerable(cancellationToken: cancellationToken);

        var count = await _collection.CountDocumentsAsync(
            filter,
            cancellationToken: cancellationToken);

        var pages = size == 0
            ? 0
            : (int)Math.Ceiling(count / (double)size);

        var pageResult = new PagedResult<TEntity>
        {
            Items = orders,
            Page = page,
            Size = size,
            TotalCount = count,
            TotalPages = pages
        };

        return pageResult;
    }
}
