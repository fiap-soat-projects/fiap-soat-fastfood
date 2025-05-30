using Application.UseCases.DTOs.Request;
using Application.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SelfServiceController : ControllerBase
{
    private readonly ICustomerSelfService _customerSelfService;

    public SelfServiceController(ICustomerSelfService customerSelfService)
    {
        _customerSelfService = customerSelfService;
    }

    [HttpGet]
    [Route("customer/{id:length(24)}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] string id, CancellationToken cancellationToken)
    {
        var response = await _customerSelfService.GetByIdAsync(id, cancellationToken);

        return Ok(response);
    }

    [HttpGet]
    [Route("customer/{cpf}")]
    public async Task<IActionResult> GetByCpfAsync([FromRoute] string cpf, CancellationToken cancellationToken)
    {
        var response = await _customerSelfService.GetByCpfAsync(cpf, cancellationToken);

        return Ok(response);
    }

    [HttpPost]
    [Route("customer")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = await _customerSelfService.RegisterAsync(request, cancellationToken);

        return Created(
            Url.Action(nameof(RegisterAsync),
            new { id = customer.Id }),
            customer);
    }
}
