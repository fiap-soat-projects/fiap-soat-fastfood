using Domain.Adapters.Interfaces;
using Domain.Repositories.Interfaces;
using Infrastructure.Clients;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Adapters;
using Infrastructure.Repositories.Interfaces;
using Inventory.Infra.Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Infra;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services
            .AddMongoDbRepositories()
            .AddMongoDbAdapters()
            .AddAdapters();
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
    
}
