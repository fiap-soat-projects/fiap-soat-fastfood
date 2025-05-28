using Infrastructure.Connections;
using Infrastructure.Connections.Interfaces;
using Infrastructure.Exceptions;
using Infrastructure.Factories;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class InfrastructureExtensions
{
    const string MONGO_CONNECTION_STRING_VARIABLE_KEY = "MongoConnectionString";
    const string APP_NAME_VARIABLE_KEY = "AppName";
    const string DEFAULT_CLUSTER_NAME = "default";

    public static IServiceCollection InjectInfrastructureDependencies(this IServiceCollection services)
    {
        services
            .AddConnections();

        return services;
    }

    private static IServiceCollection AddConnections(this IServiceCollection services)
    {
        var mongoConnectionString = Environment.GetEnvironmentVariable(MONGO_CONNECTION_STRING_VARIABLE_KEY);

        EnvironmentVariableNotFoundException.ThrowIfIsNullOrWhiteSpace(mongoConnectionString, MONGO_CONNECTION_STRING_VARIABLE_KEY);

        var appName = Environment.GetEnvironmentVariable(APP_NAME_VARIABLE_KEY);

        EnvironmentVariableNotFoundException.ThrowIfIsNullOrWhiteSpace(appName, APP_NAME_VARIABLE_KEY);

        var connection = new MongoConnection(DEFAULT_CLUSTER_NAME, mongoConnectionString!, appName);

        services
            .AddSingleton<IMongoConnection>(connection)
            .AddSingleton(MongoDataContextFactory.Create);

        return services;
    }
}
