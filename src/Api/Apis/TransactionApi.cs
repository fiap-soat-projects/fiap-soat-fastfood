using Adapter.Controllers.DTOs;
using Adapter.Controllers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Apis;

[ApiController]
[Route("/v1/Order")]
public class TransactionApi : ControllerBase
{
    private readonly IOrderController _orderController;

    public TransactionApi(IOrderController orderController)
    {
        _orderController = orderController;
    }

    [HttpPost("{id:length(24)}/checkout")]
    public async Task<IActionResult> CheckoutAsync(
        [FromBody] CheckoutRequest checkoutRequest,
        string id,
        CancellationToken cancellationToken)
    {
        var checkoutResponse = await _orderController.CheckoutAsync(id, checkoutRequest, cancellationToken);

        return Ok(checkoutResponse);
    }

    [HttpPost("{id:length(24)}/confirm-payment")]
    public async Task<IActionResult> ConfirmPaymentAsync(string id, CancellationToken cancellationToken)
    {
        await _orderController.ConfirmPaymentAsync(id, cancellationToken);

        return NoContent();
    }
}
