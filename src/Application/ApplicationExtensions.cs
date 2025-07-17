using Application.Controllers;
using Application.Controllers.Interfaces;
using Application.UseCases;
using Domain.UseCases.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application;
public static class ApplicationExtensions
{
    public static IServiceCollection InjectApplicationDependencies(this IServiceCollection services)
    {
        return services
            .RegisterUseCases()
            .RegisterControllers();
    }

    private static IServiceCollection RegisterUseCases(this IServiceCollection services)
    {

        return services
         .AddSingleton<IOrderUseCase, OrderUseCase>()
         .AddSingleton<ITransactionUseCase, TransactionService>()
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
}
