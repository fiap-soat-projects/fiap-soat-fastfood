using Application.Exceptions;
using Domain.Entities;
using Domain.Gateways.Interfaces;
using Domain.UseCases.Interfaces;

namespace Application.UseCases;

internal class CustomerUseCase : ICustomerUseCase
{
    private readonly ICustomerGateway _customerGateway;

    public CustomerUseCase(ICustomerGateway customerGateway)
    {
        _customerGateway = customerGateway;
    }

    public async Task<Customer> CreateAsync(Customer customer, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(customer, nameof(customer));

        customer = await _customerGateway.CreateAsync(customer, cancellationToken);

        return customer;
    }

    public async Task<Customer> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrEmpty(id, nameof(id));

        var customer = await _customerGateway.GetByIdAsync(id, cancellationToken);

        CustomerNotFoundException.ThrowIfNull(customer, id);

        return customer!;
    }

    public async Task<Customer> GetByCpfAsync(string cpf, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrEmpty(cpf, nameof(cpf));

        var customer = await _customerGateway.GetByCpfAsync(cpf, cancellationToken);

        CustomerNotFoundException.ThrowIfNull(customer, cpf);

        return customer!;
    }
}
