using Api.HealthChecks.DTOs;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Api.HealthChecks.Writers;

[ExcludeFromCodeCoverage]
public static class HealthCheckResponseWriter
{
    private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    public static Task Write(HttpContext context, HealthReport report)
    {
        var response = new HealthCheckResponse(report);

        return context.Response.WriteAsJsonAsync(response, _jsonSerializerOptions);
    }
}