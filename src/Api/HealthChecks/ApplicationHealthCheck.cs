using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Api.HealthChecks;

[ExcludeFromCodeCoverage]
public class ApplicationHealthCheck : IHealthCheck
{
    internal const string HEALTH_CHECK_NAME = "fast-food-api";

    private readonly Stopwatch _stopwatch = new();

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        _stopwatch.Restart();

        var data = new Dictionary<string, object>
        {
            { "time", _stopwatch.Elapsed.TotalMilliseconds }
        };

        var result = HealthCheckResult.Healthy("The application is running smoothly", data);

        return Task.FromResult(result);
    }
}
