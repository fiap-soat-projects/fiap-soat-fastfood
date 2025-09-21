using Adapter.Controllers.DTOs;
using Adapter.Controllers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

[ApiController]
[Route("/v1/self-ordering")]
public class SelfOrdering : ControllerBase
{
    private readonly ISelfOrderingController _selfOrderingController;

    public SelfOrdering(ISelfOrderingController selfOrderingController)
    {
        _selfOrderingController = selfOrderingController;
    }

    [HttpGet]
    [Route("customer/{id:length(24)}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] string id, CancellationToken cancellationToken)
    {
        var presenter = await _selfOrderingController.GetByIdAsync(id, cancellationToken);

        return Ok(presenter.ViewModel);
    }

    [HttpGet]
    [Route("customer/{cpf}")]
    public async Task<IActionResult> GetByCpfAsync([FromRoute] string cpf, CancellationToken cancellationToken)
    {
        var presenter = await _selfOrderingController.GetByCpfAsync(cpf, cancellationToken);

        return Ok(presenter.ViewModel);
    }

    [HttpPost]
    [Route("customer")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterCustomerRequest request, CancellationToken cancellationToken)
    {
        var presenter = await _selfOrderingController.RegisterAsync(request, cancellationToken);

        return Created(
            Url.Action(nameof(RegisterAsync),
            new { id = presenter.ViewModel.Id }),
            presenter.ViewModel);
    }
}
