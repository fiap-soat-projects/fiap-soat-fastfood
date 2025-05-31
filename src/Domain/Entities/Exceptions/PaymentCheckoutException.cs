using Domain.Exceptions;

namespace Domain.Entities.Exceptions;

public class PaymentCheckoutException : InvalidEntityPropertyException<PaymentCheckout>
{
    protected PaymentCheckoutException(string propertyName) : base(propertyName)
    {

    }
}
