using System.Diagnostics.CodeAnalysis;

namespace Api.HealthChecks.Monitor;

[ExcludeFromCodeCoverage]
internal static class ApplicationLifetimeMonitor
{
    public static DateTime StartDate { get; }
    public static TimeSpan UpTime { get => DateTime.UtcNow - StartDate; }

    static ApplicationLifetimeMonitor()
    {
        StartDate = DateTime.UtcNow;
    }

    internal static void Start() { }
}
