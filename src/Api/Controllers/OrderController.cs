using Application.UseCases.DTOs.Filters;
using Application.UseCases.DTOs.Request;
using Application.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderUseCase _order;

    public OrderController(IOrderUseCase order)
    {
        _order = order;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(
        [FromQuery] int page,
        [FromQuery] int size,
        [FromQuery] string? status,
        CancellationToken cancellationToken)
    {
        var orderFilter = new OrderFilter(status, page, size);

        var orders = await _order.GetAllAsync(orderFilter, cancellationToken);

        return Ok(orders);
    }

    [HttpGet("{id:length(24)}")]
    public async Task<IActionResult> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var order = await _order.GetByIdAsync(id, cancellationToken);

        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] CreateRequest createRequest, CancellationToken cancellationToken)
    {
        var id = await _order.CreateAsync(createRequest, cancellationToken);

        return new CreatedResult("/order", id);
    }

    [HttpPatch("{id:length(24)}/status")]
    public async Task<IActionResult> UpdateStatusAsync(
        [FromBody] UpdateStatusRequest request,
        string id,
        CancellationToken cancellationToken)
    {
        var updatedOrder = await _order.UpdateStatusAsync(id, request, cancellationToken);

        return Ok(updatedOrder);
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> DeleteAsync(string id, CancellationToken cancellationToken)
    {
        await _order.DeleteAsync(id, cancellationToken);

        return NoContent();
    }
}
