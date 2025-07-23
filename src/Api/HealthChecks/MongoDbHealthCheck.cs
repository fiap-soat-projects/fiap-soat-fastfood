using Infrastructure.MongoDb.Contexts.Interfaces;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Api.HealthChecks;

[ExcludeFromCodeCoverage]
public class MongoDbHealthCheck : IHealthCheck
{
    internal const string HEALTH_CHECK_NAME = "fast-food-mongodb";

    private readonly IMongoDatabase _mongoDatabase;
    private readonly Stopwatch _stopwatch;

    public MongoDbHealthCheck(IMongoContext mongoContext)
    {
        _mongoDatabase = mongoContext.Database;
        _stopwatch = new();
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var data = new Dictionary<string, object>();

        try
        {
            _stopwatch.Restart();

            _ = await _mongoDatabase.RunCommandAsync((Command<BsonDocument>)"{ping:1}", cancellationToken: cancellationToken);

            _stopwatch.Stop();

            data["time"] = _stopwatch.Elapsed.TotalMilliseconds;

            const short MONGODB_DEGRADED_TIME_LIMIT_IN_MS = 500;

            var isDegraded = _stopwatch.Elapsed.TotalMilliseconds > MONGODB_DEGRADED_TIME_LIMIT_IN_MS;

            return isDegraded
                ? HealthCheckResult.Degraded("MongoDB is running but is degraded", data: data)
                : HealthCheckResult.Healthy("MongoDB is running smoothly", data: data);
        }
        catch (Exception exception)
        {
            return new HealthCheckResult(
                context.Registration.FailureStatus,
                "MongoDB is not running",
                exception,
                data);
        }
    }
}
