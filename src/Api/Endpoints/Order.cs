using Adapter.Controllers.DTOs;
using Adapter.Controllers.DTOs.Filters;
using Adapter.Controllers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

[ApiController]
[Route("/v1/order")]
public class Order : ControllerBase
{
    private readonly IOrderController _orderController;

    public Order(IOrderController orderController)
    {
        _orderController = orderController;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] CreateRequest createRequest, CancellationToken cancellationToken)
    {
        var id = await _orderController.CreateAsync(createRequest, cancellationToken);

        return new CreatedResult("/order", id);
    }

    [HttpGet("{id:length(24)}")]
    public async Task<IActionResult> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var presenter = await _orderController.GetByIdAsync(id, cancellationToken);

        return Ok(presenter.ViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(
        [FromQuery] int page,
        [FromQuery] int size,
        [FromQuery] string? status,
        CancellationToken cancellationToken)
    {
        var orderFilter = new OrderFilter(status, page, size);

        var presenter = await _orderController.GetAllAsync(orderFilter, cancellationToken);

        return Ok(presenter.ViewModel);
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActiveAsync(
    [FromQuery] int page,
    [FromQuery] int size,
    CancellationToken cancellationToken)
    {
        var orderFilter = new OrderFilter(null, page, size);

        var presenter = await _orderController.GetActiveAsync(orderFilter, cancellationToken);

        return Ok(presenter.ViewModel);
    }

    [HttpPatch("{id:length(24)}/status")]
    public async Task<IActionResult> UpdateStatusAsync(
        [FromBody] UpdateStatusRequest request,
        string id,
        CancellationToken cancellationToken)
    {
        var presenter = await _orderController.UpdateStatusAsync(id, request, cancellationToken);

        return Ok(presenter.ViewModel);
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> DeleteAsync(string id, CancellationToken cancellationToken)
    {
        await _orderController.DeleteAsync(id, cancellationToken);

        return NoContent();
    }
}
