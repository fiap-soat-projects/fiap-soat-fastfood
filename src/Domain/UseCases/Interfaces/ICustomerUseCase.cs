using Business.Entities;

namespace Business.UseCases.Interfaces;

internal interface ICustomerUseCase
{
    Task<Customer> CreateAsync(Customer customer, CancellationToken cancellationToken);
    Task<Customer> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<Customer> GetByCpfAsync(string cpf, CancellationToken cancellationToken);
}
