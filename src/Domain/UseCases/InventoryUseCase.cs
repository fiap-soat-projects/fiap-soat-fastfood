using Business.Entities;
using Business.Exceptions;
using Business.Gateways.Loggers.Interfaces;
using Business.UseCases.Interfaces;
using System.Text;

namespace Business.UseCases;

internal class InventoryUseCase : IInventoryUseCase
{
    private readonly IInventoryLogger _inventoryLogger;

    public InventoryUseCase(IInventoryLogger inventoryLoggerGateway)
    {
        _inventoryLogger = inventoryLoggerGateway;
    }

    public void GenerateAuditLog(Order order, DateTime date)
    {
        InvalidInventoryOrderException.ThrowIfIsNotFinished(order.Status, order.Id);

        var auditLogBuilder = new StringBuilder();

        const string AUDIT_LOG_TEMPLATE = "The order {0} was finished in {1} with: {2}";

        foreach (var itemQuantity in ExtractItemQuantities(order))
        {
            var auditLog = string.Format(
                AUDIT_LOG_TEMPLATE,
                order.Id,
                date.ToString("yyyy-MM-dd HH:mm:ss"),
                itemQuantity.ToString());

            auditLogBuilder.AppendLine(auditLog);
        }

        _inventoryLogger.SendAuditLog(auditLogBuilder.ToString());
    }

    private static IEnumerable<ItemQuantity> ExtractItemQuantities(Order order)
    {
        return order
            .Items
            .GroupBy(item => item.Id)
            .Select(group => new ItemQuantity
            {
                ItemId = group.Key,
                Quantity = group.Sum(item => item.Amount)
            });
    }
}
