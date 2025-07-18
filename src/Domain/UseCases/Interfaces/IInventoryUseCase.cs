using Business.Entities;

namespace Business.UseCases.Interfaces;

internal interface IInventoryUseCase
{
    void GenerateAuditLog(Order order, DateTime date);
}
