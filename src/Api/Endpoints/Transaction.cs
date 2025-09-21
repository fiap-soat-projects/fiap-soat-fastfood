using Adapter.Controllers.DTOs;
using Adapter.Controllers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

[ApiController]
[Route("/v1/order")]
public class Transaction : ControllerBase
{
    private readonly IOrderController _orderController;

    public Transaction(IOrderController orderController)
    {
        _orderController = orderController;
    }

    [HttpPost("{id:length(24)}/checkout")]
    public async Task<IActionResult> CheckoutAsync(
        [FromBody] CheckoutRequest checkoutRequest,
        string id,
        CancellationToken cancellationToken)
    {
        var presenter = await _orderController.CheckoutAsync(id, checkoutRequest, cancellationToken);

        return Ok(presenter.ViewModel);
    }

    [HttpPost("{id:length(24)}/confirm-payment")]
    public async Task<IActionResult> ConfirmPaymentAsync(string id, CancellationToken cancellationToken)
    {
        await _orderController.ConfirmPaymentAsync(id, cancellationToken);

        return NoContent();
    }


    [HttpPost("payment/webhook")]
    public async Task<IActionResult> PaymentWebhookAsync([FromBody] PaymentWebhook webhook, CancellationToken cancellationToken)
    {
        await _orderController.ProcessPaymentAsync(webhook, cancellationToken);

        return NoContent();
    }
}
