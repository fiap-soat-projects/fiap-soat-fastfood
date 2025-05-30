using Domain.Entities;
using Domain.Services.Interfaces;

namespace Application.Services;
internal class CustomerService : ICustomerService
{
    public Task<string> CreateAsync(Customer customer, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Customer> GetByCpfAsync(string cpf, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Customer> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
