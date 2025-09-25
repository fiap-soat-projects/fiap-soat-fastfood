using Adapter.Presenters.DTOs;
using Business.Entities;

namespace Adapter.Presenters;
public class CheckoutPresenter
{
    public CheckoutResponse ViewModel { get; init; }

    public CheckoutPresenter(PaymentCheckout paymentCheckout)
    {
        ViewModel = new CheckoutResponse(
            paymentCheckout.Id,
            paymentCheckout.PaymentMethod,
            paymentCheckout.QrCode,
            paymentCheckout.QrCodeBase64,
            paymentCheckout.Amount);
    }
}
