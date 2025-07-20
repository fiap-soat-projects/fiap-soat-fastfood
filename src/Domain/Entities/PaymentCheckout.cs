using Business.Entities.Exceptions;

namespace Business.Entities;

public class PaymentCheckout
{
    private long _id;
    private string? _paymentMethod;
    private string? _qrCode;
    private string? _qrCodeBase64;
    private decimal _amount;

    public required long Id
    {
        get => _id;
        set
        {
            PaymentCheckoutException.ThrowIfIsEqualOrLowerThanZero(value, nameof(Id));

            _id = value;
        }
    }

    public required string PaymentMethod
    {
        get => _paymentMethod!;
        set
        {
            PaymentCheckoutException.ThrowIfNullOrWhiteSpace(value, nameof(PaymentMethod));

            _paymentMethod = value;
        }
    }

    public required string QrCode
    {
        get => _qrCode!;
        set
        {
            PaymentCheckoutException.ThrowIfNullOrWhiteSpace(value, nameof(PaymentMethod));

            _qrCode = value;
        }
    }

    public required string QrCodeBase64
    {
        get => _qrCodeBase64!;
        set
        {
            PaymentCheckoutException.ThrowIfNullOrWhiteSpace(value, nameof(PaymentMethod));

            _qrCodeBase64 = value;
        }
    }

    public required decimal Amount
    {
        get => _amount;
        set
        {
            PaymentCheckoutException.ThrowIfIsEqualOrLowerThanZero(value, nameof(PaymentMethod));

            _amount = value;
        }
    }
}
