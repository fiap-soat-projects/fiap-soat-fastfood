using Api.HealthChecks.Monitor;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics.CodeAnalysis;

namespace Api.HealthChecks.DTOs;

[ExcludeFromCodeCoverage]
public record HealthCheckResponse
{
    public string ServerName { get; init; }
    public DateTime ServerStart { get; init; }
    public TimeSpan ServerUpTime { get; init; }
    public string Status { get; init; }
    public Dictionary<string, object> Results { get; private set; }

    public HealthCheckResponse(HealthReport report)
    {
        ServerName = Environment.MachineName;
        ServerStart = ApplicationLifetimeMonitor.StartDate;
        ServerUpTime = ApplicationLifetimeMonitor.UpTime;
        Status = report.Status.ToString();
        Results = [];

        foreach (var (key, value) in report.Entries)
        {
            Results[key] = new
            {
                value.Status,
                value.Description,
                value.Data
            };
        }
    }
}
