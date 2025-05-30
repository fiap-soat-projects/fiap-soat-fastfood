using Application.Services;
using Application.UseCases;
using Application.UseCases.Interfaces;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Application.Extensions;

[ExcludeFromCodeCoverage]
public static class ApplicationExtensions
{
    public static IServiceCollection InjectApplicationDependencies(this IServiceCollection services)
    {
        services
            .RegisterServices()
            .RegisterUseCases();

        return services;
    }

    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<ICustomerService, CustomerService>();

        return services;
    }

    public static IServiceCollection RegisterUseCases(this IServiceCollection services)
    {
        services.AddSingleton<ICustomerSelfService, CustomerSelfService>();

        return services;
    }
}
