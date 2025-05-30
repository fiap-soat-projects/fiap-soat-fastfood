using CrossCutting.Exceptions;
using Order.Domain.Entities;

namespace Domain.Entities.Exceptions;

internal class InvalidInventoryOrderException(string message) : BaseEntityException<OrderEntity>(message)
{
    internal static void ThrowIfIsNotFinished(string orderStatus, string orderId)
    {
        const string FINISHED_ORDER_STATUS = "Finished";

        if (orderStatus is not FINISHED_ORDER_STATUS)
        {
            throw new InvalidInventoryOrderException(
                $"The order '{orderId}' should be '{FINISHED_ORDER_STATUS}' to be processed in inventory");
        }
    }
}
