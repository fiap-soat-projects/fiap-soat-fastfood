namespace Business.Gateways.Loggers.Interfaces;
public interface IInventoryLogger
{
    void SendAuditLog(string auditLog);
}
