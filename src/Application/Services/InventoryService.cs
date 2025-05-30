using Domain.Adapters.Interfaces;
using Domain.Entities;
using Domain.Entities.Exceptions;
using Domain.Services.Interfaces;
using System.Text;

namespace Application.Services;

internal class InventoryService : IInventoryService
{
    private readonly IInventoryLogger _logger;

    public InventoryService(IInventoryLogger logger)
    {
        _logger = logger;
    }

    public void RegisterOrder(
        string orderId,
        string orderStatus,
        DateTime finishDate,
        IEnumerable<ItemQuantity> itemQuantities)
    {
        InvalidInventoryOrderException.ThrowIfIsNotFinished(orderStatus, orderId!);

        var auditLogBuilder = new StringBuilder();

        const string AUDIT_LOG_TEMPLATE = "The order {0} was finished in {1} with: {2}";

        foreach (var itemQuantity in itemQuantities)
        {
            var auditLog = string.Format(
                AUDIT_LOG_TEMPLATE,
                orderId,
                finishDate.ToString("yyyy-MM-dd HH:mm:ss"),
                itemQuantity.ToString());

            auditLogBuilder.AppendLine(auditLog);
        }

        _logger.SendAuditLog(auditLogBuilder.ToString());
    }
}
