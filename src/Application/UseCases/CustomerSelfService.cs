using Application.UseCases.DTOs.Request;
using Application.UseCases.DTOs.Response;
using Application.UseCases.Interfaces;
using Domain.Entities;
using Domain.Services.Interfaces;

namespace Application.UseCases;

internal class CustomerSelfService : ICustomerSelfService
{
    private readonly ICustomerService _customerService;

    public CustomerSelfService(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public async Task<RegisterCustomerResponse> RegisterAsync(RegisterCustomerRequest input, CancellationToken cancellationToken)
    {
        var customer = new Customer(input.Name, input.Cpf, input.Email);

        customer = await _customerService.CreateAsync(customer, cancellationToken);

        return CreateResponse(customer);
    }

    public async Task<RegisterCustomerResponse> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var customer = await _customerService.GetByIdAsync(id, cancellationToken);

        return CreateResponse(customer);
    }

    public async Task<RegisterCustomerResponse> GetByCpfAsync(string cpf, CancellationToken cancellationToken)
    {
        var customer = await _customerService.GetByCpfAsync(cpf, cancellationToken);

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
