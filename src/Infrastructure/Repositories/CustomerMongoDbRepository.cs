using Infrastructure.Entities;
using Infrastructure.MongoDb.Contexts.Interfaces;
using Infrastructure.Repositories.Interfaces;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Repositories;

[ExcludeFromCodeCoverage]
internal class CustomerMongoDbRepository : BaseRepository<CustomerMongoDb>, ICustomerMongoDbRepository
{
    public CustomerMongoDbRepository(IMongoContext mongoContext) : base(mongoContext)
    {

    }

    public async Task<CustomerMongoDb> InsertOneAsync(CustomerMongoDb customerMongoDb, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(customerMongoDb, default, cancellationToken);

        return customerMongoDb;
    }

    public async Task<CustomerMongoDb?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var filter = Builders<CustomerMongoDb>
            .Filter
            .Eq(customer => customer.Id, id);

        var customer = await _collection
            .Find(filter, default)
            .FirstOrDefaultAsync(cancellationToken);

        return customer;
    }

    public async Task<CustomerMongoDb?> GetByCpfAsync(string cpf, CancellationToken cancellationToken)
    {
        var filter = Builders<CustomerMongoDb>
            .Filter
            .Eq(customer => customer.Cpf, cpf);

        var customer = await _collection
            .Find(filter, default)
            .FirstOrDefaultAsync(cancellationToken);

        return customer;
    }
}
