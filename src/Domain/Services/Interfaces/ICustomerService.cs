using Domain.Entities;

namespace Domain.Services.Interfaces;

public interface ICustomerService
{
    Task<Customer> GetByCpfAsync(string cpf, CancellationToken cancellationToken);
    Task<Customer> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<string> CreateAsync(Customer customer, CancellationToken cancellationToken);
}