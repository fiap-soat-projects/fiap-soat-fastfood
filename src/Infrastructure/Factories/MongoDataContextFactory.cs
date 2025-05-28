using Infrastructure.Connections.Interfaces;
using Infrastructure.Contexts;
using Infrastructure.Contexts.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Infrastructure.Factories;

public class MongoDataContextFactory
{
    const string DEFAULT_CLUSTER_NAME = "default";

    public static IMongoContext Create(IServiceProvider serviceProvider)
    {
        var mongoConnection = serviceProvider
            .GetServices<IMongoConnection>()
            .First(connection => connection.AppName == DEFAULT_CLUSTER_NAME);

        var mongoDatabase = mongoConnection
            .Client
            .GetDatabase(mongoConnection.MongoUrl.DatabaseName);

        return new MongoContext(DEFAULT_CLUSTER_NAME, mongoDatabase);
    }
}
