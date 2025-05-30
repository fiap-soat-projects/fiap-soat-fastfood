using Domain.Adapters.Interfaces;
using Domain.Repositories.Interfaces;
using Infrastructure.Clients;
using Infrastructure.Connections;
using Infrastructure.Connections.Interfaces;
using Infrastructure.Exceptions;
using Infrastructure.Factories;
using Infrastructure.Logger;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Adapters;
using Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using System.Net.Http.Headers;

namespace Infrastructure;

public static class InfrastructureExtensions
{
    const string MONGO_CONNECTION_STRING_VARIABLE_KEY = "MongoConnectionString";
    const string APP_NAME_VARIABLE_KEY = "AppName";
    const string DEFAULT_CLUSTER_NAME = "default";

    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services
            .AddMongoDbRepositories()
            .AddMongoDbAdapters()
            .AddAdapters()
            .RegisterConnections()
            .AddClients();
    }

    public static IServiceCollection AddMongoDbRepositories(this IServiceCollection services)
    {
        services
            .AddSingleton<IInventoryLogger, InventoryLogger>()
            .AddSingleton<IOrderMongoDbRepository, OrderMongoDbRepository>();

        return services;
    }

    public static IServiceCollection AddMongoDbAdapters(this IServiceCollection services)
    {
        services
            .AddSingleton<IOrderRepository, OrderRepository>();

        return services;
    }

    public static IServiceCollection AddAdapters(this IServiceCollection services)
    {
        services
            .AddSingleton<IPixAdapter, MercadoPagoClient>() 
            .AddSingleton<IInventoryLogger, InventoryLogger>();

        return services;
    }

    private static IServiceCollection RegisterConnections(this IServiceCollection services)
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

    public static IServiceCollection AddClients(this IServiceCollection services)
    {
        const int RETRY_COUNT = 3;

        var mercadoPagoApiUrl = Environment.GetEnvironmentVariable("MERCADOPAGO_API_URL");
        var mercadoPagoApiToken = Environment.GetEnvironmentVariable("MERCADOPAGO_API_TOKEN");

        services.AddHttpClient<IPixAdapter, MercadoPagoClient>(client =>
        {
            client.BaseAddress = new Uri(mercadoPagoApiUrl!);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", mercadoPagoApiToken);
        }).AddTransientHttpErrorPolicy(policyBuilder =>
        {
            return policyBuilder.WaitAndRetryAsync(
                RETRY_COUNT,
                attempt => TimeSpan.FromSeconds(0.4 * Math.Pow(2, attempt)));
        });

        return services;
    }
}
