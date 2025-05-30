using Domain.Entities;

namespace Domain.Services.Interfaces;

public interface IInventoryService
{
    void RegisterOrder(string orderId, string orderStatus, DateTime finishDate, IEnumerable<ItemQuantity> itemQuantities);
}
