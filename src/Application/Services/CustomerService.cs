using Application.Exceptions;
using Domain.Adapters.Repositories;
using Domain.Entities;
using Domain.Services.Interfaces;

namespace Application.Services;

internal class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Customer> CreateAsync(Customer customer, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(customer, nameof(customer));

        customer = await _customerRepository.CreateAsync(customer, cancellationToken);

        return customer;
    }

    public async Task<Customer> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrEmpty(id, nameof(id));

        var customer = await _customerRepository.GetByIdAsync(id, cancellationToken);

        CustomerNotFoundException.ThrowIfNull(customer, id);

        return customer!;
    }

    public async Task<Customer> GetByCpfAsync(string cpf, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrEmpty(cpf, nameof(cpf));

        var customer = await _customerRepository.GetByCpfAsync(cpf, cancellationToken);

        CustomerNotFoundException.ThrowIfNull(customer, cpf);

        return customer!;
    }
}
