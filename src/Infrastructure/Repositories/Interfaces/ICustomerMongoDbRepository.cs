using Infrastructure.Entities;

namespace Infrastructure.Repositories.Interfaces;

internal interface ICustomerMongoDbRepository
{
    Task<CustomerMongoDb> InsertOneAsync(CustomerMongoDb customerMongoDb, CancellationToken cancellationToken);
    Task<CustomerMongoDb?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<CustomerMongoDb?> GetByCpfAsync(string cpf, CancellationToken cancellationToken);
}
