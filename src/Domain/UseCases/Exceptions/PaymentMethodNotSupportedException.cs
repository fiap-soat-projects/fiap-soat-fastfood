using Business.Entities.Enums;

namespace Business.UseCases.Exceptions;
internal class PaymentMethodNotSupportedException : Exception
{
    private const string DEFAULT_MESSAGE = "Payment method '{0}' not supported.";

    internal PaymentMethodNotSupportedException(string paymentMethod)
        : base(string.Format(DEFAULT_MESSAGE, paymentMethod))
    {
    }

    internal static void ThrowIfPaymentMethodIsNotSupported(string paymentMethod)
    {
        var isInvalid =
            Enum.TryParse(paymentMethod, out PaymentMethod paymentMethodEnum) is false
            || paymentMethodEnum != PaymentMethod.Pix;

        if (isInvalid)
        {
            throw new PaymentMethodNotSupportedException(paymentMethod);
        }
    }
}
