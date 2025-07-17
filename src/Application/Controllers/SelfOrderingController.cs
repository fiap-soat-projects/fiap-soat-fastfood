using Application.Controllers.DTOs.Request;
using Application.Controllers.DTOs.Response;
using Application.Controllers.Interfaces;
using Domain.Entities;
using Domain.UseCases.Interfaces;

namespace Application.Controllers;

internal class SelfOrderingController : ISelfOrderingController
{
    private readonly ICustomerUseCase _customerUseCase;

    public SelfOrderingController(ICustomerUseCase customerUseCase)
    {
        _customerUseCase = customerUseCase;
    }

    public async Task<RegisterCustomerResponse> RegisterAsync(RegisterCustomerRequest input, CancellationToken cancellationToken)
    {
        var customer = new Customer(input.Name, input.Cpf, input.Email);

        customer = await _customerUseCase.CreateAsync(customer, cancellationToken);

        return CreateResponse(customer);
    }

    public async Task<RegisterCustomerResponse> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var customer = await _customerUseCase.GetByIdAsync(id, cancellationToken);

        return CreateResponse(customer);
    }

    public async Task<RegisterCustomerResponse> GetByCpfAsync(string cpf, CancellationToken cancellationToken)
    {
        var customer = await _customerUseCase.GetByCpfAsync(cpf, cancellationToken);

        return CreateResponse(customer);
    }

    private static RegisterCustomerResponse CreateResponse(Customer customer)
    {
        return new RegisterCustomerResponse
        {
            Id = customer.Id,
            CreatedAt = customer.CreatedAt,
            Name = customer.Name,
            Cpf = customer.Cpf,
            Email = customer.Email
        };
    }
}
