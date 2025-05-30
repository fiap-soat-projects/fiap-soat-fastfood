using Domain.Entities;

namespace Domain.Services.Interfaces;

internal interface ICustomerService
{
    Task<Customer> CreateAsync(Customer customer, CancellationToken cancellationToken);
    Task<Customer> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<Customer> GetByCpfAsync(string cpf, CancellationToken cancellationToken);
}
