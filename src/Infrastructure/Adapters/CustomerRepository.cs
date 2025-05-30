using Domain.Adapters.Repositories;
using Domain.Entities;
using Infrastructure.Repositories.Entities;
using Infrastructure.Repositories.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Adapters;

[ExcludeFromCodeCoverage]
internal class CustomerRepository : ICustomerRepository
{
    private readonly ICustomerMongoDbRepository _repository;

    public CustomerRepository(ICustomerMongoDbRepository repository)
    {
        _repository = repository;
    }

    public async Task<Customer> CreateAsync(Customer customer, CancellationToken cancellationToken)
    {
        var customerMongoDb = new CustomerMongoDb(customer);

        customerMongoDb = await _repository.InsertOneAsync(customerMongoDb, cancellationToken);

        return customerMongoDb.ToEntity();
    }

    public async Task<Customer?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var customerMongoDb = await _repository.GetByIdAsync(id, cancellationToken);

        if (customerMongoDb is null)
        {
            return default!;
        }

        return customerMongoDb.ToEntity();
    }

    public async Task<Customer?> GetByCpfAsync(string cpf, CancellationToken cancellationToken)
    {
        var customerMongoDb = await _repository.GetByCpfAsync(cpf, cancellationToken);

        if (customerMongoDb is null)
        {
            return default!;
        }

        return customerMongoDb.ToEntity();
    }
}
