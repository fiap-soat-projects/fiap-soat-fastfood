using Domain.Entities;

namespace Domain.Gateways.Interfaces;

internal interface ICustomerGateway
{
    Task<Customer> CreateAsync(Customer customer, CancellationToken cancellationToken);
    Task<Customer?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<Customer?> GetByCpfAsync(string cpf, CancellationToken cancellationToken);
}
