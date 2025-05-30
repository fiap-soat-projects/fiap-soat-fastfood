namespace Domain.Adapters.Interfaces;
public interface IInventoryLogger
{
    void SendAuditLog(string auditLog);
}
