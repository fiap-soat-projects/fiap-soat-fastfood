using Domain.Adapters.Interfaces;
using Inventory.Infra.Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Infra;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .AddSingleton<IInventoryLogger, InventoryLogger>();

        return services;
    }
}
