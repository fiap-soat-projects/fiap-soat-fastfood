using Business.Exceptions;

namespace Business.Entities.Exceptions;

public class PaymentCheckoutException : InvalidEntityPropertyException<PaymentCheckout>
{
    protected PaymentCheckoutException(string propertyName) : base(propertyName)
    {

    }
}
