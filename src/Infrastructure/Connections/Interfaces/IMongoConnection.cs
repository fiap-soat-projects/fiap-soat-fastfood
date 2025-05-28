using MongoDB.Driver;

namespace Infrastructure.Connections.Interfaces;

internal interface IMongoConnection
{
    public string? AppName { get; }
    public string ClusterName { get; }
    public MongoUrl MongoUrl { get; }
    public IMongoClient Client { get; }
}
