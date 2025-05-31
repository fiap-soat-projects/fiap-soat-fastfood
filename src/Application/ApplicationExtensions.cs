using Application.Services;
using Application.UseCases;
using Application.UseCases.Interfaces;
using Domain.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application;
public static class ApplicationExtensions
{
    public static IServiceCollection InjectApplicationDependencies(this IServiceCollection services)
    {
        return services
            .RegisterUseCases()
            .RegisterServices();
    }

    private static IServiceCollection RegisterUseCases(this IServiceCollection services)
    {
        return services
            .AddSingleton<IOrderUseCase, OrderUseCase>()
            .AddSingleton<ISelfServiceUseCase, SelfServiceUseCase>()
            .AddSingleton<IMenuUseCase, MenuUseCase>();
    }

    private static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        return services
            .AddSingleton<IOrderService, OrderService>()
            .AddSingleton<IPaymentService, PaymentService>()
            .AddSingleton<ICustomerService, CustomerService>()
            .AddSingleton<IInventoryService, InventoryService>()
            .AddSingleton<IMenuItemService, MenuItemService>();
    }
}
