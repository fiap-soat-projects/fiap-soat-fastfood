using Application.UseCases.DTOs.Filters;
using Application.UseCases.DTOs.Request;
using Application.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MenuController : ControllerBase
{
    private readonly IMenuUseCase _menu;

    public MenuController(IMenuUseCase menu)
    {
        _menu = menu;
    }

    [HttpGet("{id:length(24)}")]
    public async Task<IActionResult> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var response = await _menu.GetByIdAsync(id, cancellationToken);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] MenuFilter filter, CancellationToken cancellationToken)
    {
        var menuItems = await _menu.GetAllAsync(filter, cancellationToken);

        return Ok(menuItems);
    }

    [HttpPost]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterMenuItemRequest request, CancellationToken cancellationToken)
    {
        var response = await _menu.RegisterAsync(request, cancellationToken);

        return Created(
            Url.Action(nameof(RegisterAsync),
            new { id = response.Id }),
            response);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> UpdateAsync(string id, [FromBody] UpdateMenuItemRequest request, CancellationToken cancellationToken)
    {
        var response = await _menu.UpdateAsync(id, request, cancellationToken);

        return Ok(response);
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> SoftDeleteAsync(string id, CancellationToken cancellationToken)
    {
        await _menu.SoftDeleteAsync(id, cancellationToken);

        return NoContent();
    }
}
