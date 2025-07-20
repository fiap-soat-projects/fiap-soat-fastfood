namespace Business.UseCases.Exceptions;
internal class InvalidOrderStatusFilterException : Exception
{
    private const string DEFAULT_MESSAGE = "Invalid order status filter provided. Value: {0}";
    public InvalidOrderStatusFilterException(string status)
        : base(string.Format(DEFAULT_MESSAGE, status))
    {
    }
}
