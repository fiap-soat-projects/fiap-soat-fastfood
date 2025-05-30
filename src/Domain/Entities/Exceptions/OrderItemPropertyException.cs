namespace Domain.Entities.Exceptions;
internal class OrderItemPropertyException : BaseEntityException<OrderItem>
{
    public OrderItemPropertyException(string propertyName) : base(propertyName)
    {
    }

    internal static int ThrowIfZeroOrNegative(int value, string propertyName)
    {
        if(value < 1)
        {
            throw new OrderItemPropertyException(propertyName);
        }

        return value;
    }

    internal static decimal ThrowIfZeroOrNegative(decimal value, string propertyName)
    {
        if (value < 0.00M)
        {
            throw new OrderItemPropertyException(propertyName);
        }
        return value;
    }
}
