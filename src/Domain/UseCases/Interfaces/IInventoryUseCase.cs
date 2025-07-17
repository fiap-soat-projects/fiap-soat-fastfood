using Domain.Entities;

namespace Domain.UseCases.Interfaces;

public interface IInventoryUseCase
{
    void GenerateAuditLog(Order order, DateTime date);
}
