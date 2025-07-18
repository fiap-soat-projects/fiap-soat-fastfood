using Business.Entities.Enums;

namespace Business.Exceptions;

public class InvalidInventoryOrderException(string message, OrderStatus status) : DomainException(message)
{
    public OrderStatus InvalidStatus { get; private set; } = status;

    public static void ThrowIfIsNotFinished(OrderStatus status, string orderId)
    {
        const string FINISHED_ORDER_STATUS = "Finished";
        const string INVALID_INVENTORY_ORDER_MESSAGE_TEMPLATE = "The order '{0}' should be '{1}' to be processed in inventory";

        if (status.ToString() is not FINISHED_ORDER_STATUS)
        {
            throw new InvalidInventoryOrderException(
                string.Format(INVALID_INVENTORY_ORDER_MESSAGE_TEMPLATE, orderId, FINISHED_ORDER_STATUS),
                status);
        }
    }
}
