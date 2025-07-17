using Application.Controllers.DTOs.Request;
using Application.Controllers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

[ApiController]
[Route("/v1/[controller]")]
public class SelfOrderingEndpoint : ControllerBase
{
    private readonly ISelfOrderingController _selfOrderingController;

    public SelfOrderingEndpoint(ISelfOrderingController selfOrderingController)
    {
        _selfOrderingController = selfOrderingController;
    }

    [HttpGet]
    [Route("customer/{id:length(24)}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] string id, CancellationToken cancellationToken)
    {
        var response = await _selfOrderingController.GetByIdAsync(id, cancellationToken);

        return Ok(response);
    }

    [HttpGet]
    [Route("customer/{cpf}")]
    public async Task<IActionResult> GetByCpfAsync([FromRoute] string cpf, CancellationToken cancellationToken)
    {
        var response = await _selfOrderingController.GetByCpfAsync(cpf, cancellationToken);

        return Ok(response);
    }

    [HttpPost]
    [Route("customer")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = await _selfOrderingController.RegisterAsync(request, cancellationToken);

        return Created(
            Url.Action(nameof(RegisterAsync),
            new { id = customer.Id }),
            customer);
    }
}
