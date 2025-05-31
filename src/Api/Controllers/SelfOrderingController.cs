using Application.UseCases.DTOs.Request;
using Application.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("/v1/[controller]")]
public class SelfOrderingController : ControllerBase
{
    private readonly ISelfOrderingUseCase _selfOrdering;

    public SelfOrderingController(ISelfOrderingUseCase selfService)
    {
        _selfOrdering = selfService;
    }

    [HttpGet]
    [Route("customer/{id:length(24)}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] string id, CancellationToken cancellationToken)
    {
        var response = await _selfOrdering.GetByIdAsync(id, cancellationToken);

        return Ok(response);
    }

    [HttpGet]
    [Route("customer/{cpf}")]
    public async Task<IActionResult> GetByCpfAsync([FromRoute] string cpf, CancellationToken cancellationToken)
    {
        var response = await _selfOrdering.GetByCpfAsync(cpf, cancellationToken);

        return Ok(response);
    }

    [HttpPost]
    [Route("customer")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = await _selfOrdering.RegisterAsync(request, cancellationToken);

        return Created(
            Url.Action(nameof(RegisterAsync),
            new { id = customer.Id }),
            customer);
    }
}
