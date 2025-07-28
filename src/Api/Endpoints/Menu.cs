using Adapter.Controllers.DTOs;
using Adapter.Controllers.DTOs.Filters;
using Adapter.Controllers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

[ApiController]
[Route("/v1/menu")]
public class Menu : ControllerBase
{
    private readonly IMenuController _menuController;

    public Menu(IMenuController menuController)
    {
        _menuController = menuController;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterMenuItemRequest request, CancellationToken cancellationToken)
    {
        var response = await _menuController.RegisterAsync(request, cancellationToken);

        return Created(
            Url.Action(nameof(RegisterAsync),
            new { id = response.Id }),
            response);
    }

    [HttpGet("{id:length(24)}")]
    public async Task<IActionResult> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var response = await _menuController.GetByIdAsync(id, cancellationToken);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] MenuFilter filter, CancellationToken cancellationToken)
    {
        var menuItems = await _menuController.GetAllAsync(filter, cancellationToken);

        return Ok(menuItems);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> UpdateAsync(string id, [FromBody] UpdateMenuItemRequest request, CancellationToken cancellationToken)
    {
        var response = await _menuController.UpdateAsync(id, request, cancellationToken);

        return Ok(response);
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> SoftDeleteAsync(string id, CancellationToken cancellationToken)
    {
        await _menuController.SoftDeleteAsync(id, cancellationToken);

        return NoContent();
    }
}
