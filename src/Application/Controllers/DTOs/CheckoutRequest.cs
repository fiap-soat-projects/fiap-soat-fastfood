using System.Globalization;

namespace Adapter.Controllers.DTOs;

public record CheckoutRequest
{
    private string? _paymentType;

    public string? PaymentType
    {
        get => _paymentType;
        init
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);

            var paymentType = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());

            _paymentType = paymentType;
        }
    }
}
