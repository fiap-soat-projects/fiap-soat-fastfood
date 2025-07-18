using Business.Entities;
using Business.Exceptions;
using Business.Gateways.Repositories.Interfaces;
using Infrastructure.Entities;
using Infrastructure.Repositories.Interfaces;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace Adapter.Gateways.Repositories;

[ExcludeFromCodeCoverage]
internal class CustomerGateway : ICustomerRepository
{
    private readonly ICustomerMongoDbRepository _customerMongoDbRepository;

    public CustomerGateway(ICustomerMongoDbRepository customerMongoDbRepository)
    {
        _customerMongoDbRepository = customerMongoDbRepository;
    }

    public async Task<Customer> CreateAsync(Customer customer, CancellationToken cancellationToken)
    {
        try
        {
            var customerMongoDb = new CustomerMongoDb(customer);

            customerMongoDb = await _customerMongoDbRepository.InsertOneAsync(customerMongoDb, cancellationToken);

            return customerMongoDb.ToDomain();
        }
        catch (MongoWriteException exception) when (exception.WriteError.Category is ServerErrorCategory.DuplicateKey)
        {
            throw new DuplicatedItemException<Customer>(nameof(Customer.Cpf));
        }
    }

    public async Task<Customer?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var customerMongoDb = await _customerMongoDbRepository.GetByIdAsync(id, cancellationToken);

        if (customerMongoDb is null)
        {
            return default!;
        }

        return customerMongoDb.ToDomain();
    }

    public async Task<Customer?> GetByCpfAsync(string cpf, CancellationToken cancellationToken)
    {
        var customerMongoDb = await _customerMongoDbRepository.GetByCpfAsync(cpf, cancellationToken);

        if (customerMongoDb is null)
        {
            return default!;
        }

        return customerMongoDb.ToDomain();
    }
}
