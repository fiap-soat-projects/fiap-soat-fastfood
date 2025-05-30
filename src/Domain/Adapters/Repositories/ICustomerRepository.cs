using Domain.Entities;

namespace Domain.Adapters.Repositories;

internal interface ICustomerRepository
{
    Task<Customer> CreateAsync(Customer customer, CancellationToken cancellationToken);
    Task<Customer?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<Customer?> GetByCpfAsync(string cpf, CancellationToken cancellationToken);
}
