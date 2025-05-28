using MongoDB.Driver;

namespace Infrastructure.Contexts.Interfaces;

public interface IMongoContext
{
    public string ClusterName { get; }
    public IMongoDatabase Database { get; }
    IMongoCollection<TEntity> GetCollection<TEntity>();
    IMongoCollection<TEntity> GetCollection<TEntity>(string collectionName);
}
