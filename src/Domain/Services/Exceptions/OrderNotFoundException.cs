using Domain.Entities;

namespace Domain.Services.Exceptions;
internal class OrderNotFoundException : Exception
{
    private const string DEFAULT_MESSAGE = "Order with ID '{0}' not found.";

    internal OrderNotFoundException(string orderId)
        : base(string.Format(DEFAULT_MESSAGE, orderId))
    {
    }

    internal static void ThrowIfNullOrEmpty(string orderId, Order? orderEntity)
    {
        if (orderEntity is null)
        {
            throw new OrderNotFoundException(orderId);
        }
    }
}

