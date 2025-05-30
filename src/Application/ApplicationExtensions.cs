using Application.Services;
using Application.UseCases;
using Application.UseCases.Interfaces;
using Domain.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application;
public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddUseCases()
            .AddServices();
    }

    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        return services.AddSingleton<IOrderUseCase, OrderUseCase>();
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddSingleton<IOrderService, OrderService>()
            .AddSingleton<IPaymentService, PaymentService>()
            .AddSingleton<ICustomerService, CustomerService>()
            .AddSingleton<IInventoryService, InventoryService>();
    }
}
