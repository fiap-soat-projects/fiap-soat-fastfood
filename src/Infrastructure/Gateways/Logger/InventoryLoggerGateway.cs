using Domain.Gateways.Interfaces;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Gateways.Logger;

[ExcludeFromCodeCoverage]
internal class InventoryLoggerGateway : IInventoryLoggerGateway
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
