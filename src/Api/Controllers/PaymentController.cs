using Application.UseCases.DTOs.Request;
using Application.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("Order")]
public class PaymentController : ControllerBase
{
    private readonly IOrderUseCase _orderUseCase;

    public PaymentController(IOrderUseCase orderService)
    {
        _orderUseCase = orderService;
    }

    [HttpPost("{id}/checkout")]
    public async Task<IActionResult> CheckoutAsync(
        [FromBody] CheckoutRequest checkoutRequest,
        string id,
        CancellationToken cancellationToken)
    {
        var checkoutResponse = await _orderUseCase.CheckoutAsync(id, checkoutRequest, cancellationToken);
        return Ok(checkoutResponse);
    }

    [HttpPost("{id}/confirm-payment")]
    public async Task<IActionResult> ConfirmPaymentAsync(
        string id,
        CancellationToken cancellationToken)
    {
        await _orderUseCase.ConfirmPaymentAsync(id, cancellationToken);
        return NoContent();
    }
}
