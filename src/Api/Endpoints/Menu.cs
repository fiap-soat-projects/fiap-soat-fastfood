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
        var presenter = await _menuController.RegisterAsync(request, cancellationToken);

        return Created(
            Url.Action(nameof(RegisterAsync),
            new { id = presenter.ViewModel.Id }),
            presenter.ViewModel);
    }

    [HttpGet("{id:length(24)}")]
    public async Task<IActionResult> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var presenter = await _menuController.GetByIdAsync(id, cancellationToken);

        return Ok(presenter.ViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] MenuFilter filter, CancellationToken cancellationToken)
    {
        var presenter = await _menuController.GetAllAsync(filter, cancellationToken);

        return Ok(presenter.ViewModel);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> UpdateAsync(string id, [FromBody] UpdateMenuItemRequest request, CancellationToken cancellationToken)
    {
        var presenter = await _menuController.UpdateAsync(id, request, cancellationToken);

        return Ok(presenter.ViewModel);
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> SoftDeleteAsync(string id, CancellationToken cancellationToken)
    {
        await _menuController.SoftDeleteAsync(id, cancellationToken);

        return NoContent();
    }
}
