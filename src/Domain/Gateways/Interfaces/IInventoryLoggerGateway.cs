namespace Domain.Gateways.Interfaces;
public interface IInventoryLoggerGateway
{
    void SendAuditLog(string auditLog);
}
