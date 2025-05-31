using Domain.Adapters.Interfaces;
using Domain.Adapters.Repositories;
using Infrastructure.Adapters.Clients;
using Infrastructure.Adapters.Logger;
using Infrastructure.Adapters.Repositories;
using Infrastructure.Exceptions;
using Infrastructure.MongoDb.Connections;
using Infrastructure.MongoDb.Connections.Interfaces;
using Infrastructure.MongoDb.Factories;
using Infrastructure.MongoDb.Options;
using Infrastructure.Repositories;
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

    public static IServiceCollection InjectInfrastructureDependencies(this IServiceCollection services)
    {
        services
            .RegisterMongoDbRepositories()
            .RegisterMongoDbAdapters()
            .RegisterAdapters()
            .RegisterConnections()
            .RegisterClients();

        MongoGlobalOptions.Init();

        return services;
    }

    public static IServiceCollection RegisterMongoDbRepositories(this IServiceCollection services)
    {
        services
            .AddSingleton<IInventoryLogger, InventoryLogger>()
            .AddSingleton<IOrderMongoDbRepository, OrderMongoDbRepository>()
            .AddSingleton<ICustomerMongoDbRepository, CustomerMongoDbRepository>()
            .AddSingleton<IMenuItemMongoDbRepository, MenuItemMongoDbRepository>();

        return services;
    }

    public static IServiceCollection RegisterMongoDbAdapters(this IServiceCollection services)
    {
        services
            .AddSingleton<IOrderRepository, OrderRepository>()
            .AddSingleton<ICustomerRepository, CustomerRepository>()
            .AddSingleton<IMenuItemRepository, MenuItemRepository>();

        return services;
    }

    public static IServiceCollection RegisterAdapters(this IServiceCollection services)
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

    public static IServiceCollection RegisterClients(this IServiceCollection services)
    {
        const string MERCADO_PAGO_API_URL_KEY = "MERCADOPAGO_API_URL";
        const string MERCADO_PAGO_API_TOKEN_KEY = "MERCADOPAGO_API_TOKEN";
        const int RETRY_COUNT = 3;

        var mercadoPagoApiUrl = Environment.GetEnvironmentVariable(MERCADO_PAGO_API_URL_KEY);
        EnvironmentVariableNotFoundException.ThrowIfIsNullOrWhiteSpace(mercadoPagoApiUrl, MERCADO_PAGO_API_URL_KEY);

        var mercadoPagoApiToken = Environment.GetEnvironmentVariable(MERCADO_PAGO_API_TOKEN_KEY);
        EnvironmentVariableNotFoundException.ThrowIfIsNullOrWhiteSpace(mercadoPagoApiToken, MERCADO_PAGO_API_TOKEN_KEY);

        services.AddHttpClient<IPixAdapter, MercadoPagoClient>(client =>
        {
            client.BaseAddress = new Uri(mercadoPagoApiUrl!);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", mercadoPagoApiToken);
        })
        .AddTransientHttpErrorPolicy(policyBuilder =>
        {
            return policyBuilder.WaitAndRetryAsync(
                RETRY_COUNT,
                attempt => TimeSpan.FromSeconds(0.4 * Math.Pow(2, attempt)));
        });

        return services;
    }
}
