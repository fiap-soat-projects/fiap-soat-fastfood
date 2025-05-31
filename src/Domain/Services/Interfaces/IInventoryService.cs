using Domain.Entities;

namespace Domain.Services.Interfaces;

public interface IInventoryService
{
    void GenerateAuditLog(Order order, DateTime date);
}
