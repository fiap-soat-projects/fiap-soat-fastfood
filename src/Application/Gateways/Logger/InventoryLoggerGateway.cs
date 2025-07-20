using Business.Gateways.Loggers.Interfaces;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace Adapter.Gateways.Logger;

[ExcludeFromCodeCoverage]
internal class InventoryLoggerGateway : IInventoryLogger
{
    private readonly ILogger<InventoryLoggerGateway> _logger;

    public InventoryLoggerGateway(ILogger<InventoryLoggerGateway> logger)
    {
        _logger = logger;
    }

    public void SendAuditLog(string auditLog)
    {
        _logger.LogInformation("{AuditLog}", auditLog);
    }
}
