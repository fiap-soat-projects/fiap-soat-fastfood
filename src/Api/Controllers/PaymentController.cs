using Application.UseCases.DTOs.Request;
using Application.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("Order")]
public class PaymentController : ControllerBase
{
    private readonly IOrderUseCase _order;

    public PaymentController(IOrderUseCase order)
    {
        _order = order;
    }

    [HttpPost("{id:length(24)}/checkout")]
    public async Task<IActionResult> CheckoutAsync(
        [FromBody] CheckoutRequest checkoutRequest,
        string id,
        CancellationToken cancellationToken)
    {
        var checkoutResponse = await _order.CheckoutAsync(id, checkoutRequest, cancellationToken);

        return Ok(checkoutResponse);
    }

    [HttpPost("{id:length(24)}/confirm-payment")]
    public async Task<IActionResult> ConfirmPaymentAsync(string id, CancellationToken cancellationToken)
    {
        await _order.ConfirmPaymentAsync(id, cancellationToken);

        return NoContent();
    }
}
