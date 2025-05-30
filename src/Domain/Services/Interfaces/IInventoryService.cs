using Domain.Entities;

namespace Domain.Services.Interfaces;

/// <summary>
/// Simulates the registering of Orders in the inventory thought the generation of auditable logs
/// </summary>
public interface IInventoryService
{
    void RegisterOrder(
        string orderId,
        string orderStatus,
        DateTime finishDate,
        IEnumerable<ItemQuantity> itemQuantities);
}
