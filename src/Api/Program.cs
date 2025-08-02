using Adapter;
using Api.HealthChecks;
using Api.HealthChecks.Monitor;
using Api.HealthChecks.Writers;
using Api.Middlewares;
using Infrastructure;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Prometheus;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Api;

[ExcludeFromCodeCoverage]
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var services = builder.Services;

        ConfigureDependencies(services);

        var app = builder.Build();

        ConfigureMiddlewares(app);
        MapHealthChecks(app);
        ConfigureApplication(app);

        ApplicationLifetimeMonitor.Start();

        app.Run();
    }

    private static void ConfigureDependencies(IServiceCollection services)
    {
        services
            .InjectInfrastructureDependencies()
            .InjectAdapterDependencies();

        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

        services
            .AddHealthChecks()
            .AddCheck<ApplicationHealthCheck>(ApplicationHealthCheck.HEALTH_CHECK_NAME)
            .AddCheck<MongoDbHealthCheck>(MongoDbHealthCheck.HEALTH_CHECK_NAME);
    }

    private static void ConfigureMiddlewares(WebApplication app)
    {
        app.UseMiddleware<ErrorHandlingMiddleware>();
    }

    private static void MapHealthChecks(WebApplication app)
    {
        app.MapHealthChecks("/healthz", new HealthCheckOptions
        {
            Predicate = registration => registration.Name == ApplicationHealthCheck.HEALTH_CHECK_NAME,
            ResponseWriter = HealthCheckResponseWriter.Write,
            ResultStatusCodes = { [HealthStatus.Healthy] = StatusCodes.Status200OK }
        });

        app.MapHealthChecks("/health", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = HealthCheckResponseWriter.Write,
            ResultStatusCodes =
            {
                [HealthStatus.Healthy] = StatusCodes.Status200OK,
                [HealthStatus.Degraded] = StatusCodes.Status200OK,
                [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
            }
        });
    }

    private static void ConfigureApplication(WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.UseHttpMetrics();
        app.MapMetrics();
    }
}