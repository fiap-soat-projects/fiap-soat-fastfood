using Adapter.Controllers;
using Adapter.Controllers.Interfaces;
using Adapter.Gateways.Logger;
using Adapter.Gateways.Repositories;
using Business.Gateways.Loggers.Interfaces;
using Business.Gateways.Repositories.Interfaces;
using Business.UseCases;
using Business.UseCases.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Adapter;
public static class AdapterExtensions
{
    public static IServiceCollection InjectAdapterDependencies(this IServiceCollection services)
    {
        return services
            .RegisterUseCases()
            .RegisterControllers()
            .RegisterGateways();
    }

    private static IServiceCollection RegisterUseCases(this IServiceCollection services)
    {

        return services
         .AddSingleton<IOrderUseCase, OrderUseCase>()
         .AddSingleton<ITransactionUseCase, TransactionUseCase>()
         .AddSingleton<ICustomerUseCase, CustomerUseCase>()
         .AddSingleton<IInventoryUseCase, InventoryUseCase>()
         .AddSingleton<IMenuItemUseCase, MenuItemUseCase>();

    }

    private static IServiceCollection RegisterControllers(this IServiceCollection services)
    {
        return services
             .AddSingleton<IOrderController, OrderController>()
             .AddSingleton<ISelfOrderingController, SelfOrderingController>()
             .AddSingleton<IMenuController, MenuController>();
    }

    public static IServiceCollection RegisterGateways(this IServiceCollection services)
    {
        services
            .AddSingleton<IInventoryLogger, InventoryLoggerGateway>()
            .AddSingleton<IOrderRepository, OrderGateway>()
            .AddSingleton<ICustomerRepository, CustomerGateway>()
            .AddSingleton<IMenuItemRepository, MenuItemGateway>();

        return services;
    }
}
