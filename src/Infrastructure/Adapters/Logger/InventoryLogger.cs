using Domain.Adapters.Interfaces;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Adapters.Logger;

[ExcludeFromCodeCoverage]
internal class InventoryLogger : IInventoryLogger
{
    private readonly ILogger<InventoryLogger> _logger;

    public InventoryLogger(ILogger<InventoryLogger> logger)
    {
        _logger = logger;
    }

    public void SendAuditLog(string auditLog)
    {
        _logger.LogInformation("{AuditLog}", auditLog);
    }
}
