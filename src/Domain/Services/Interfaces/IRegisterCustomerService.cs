using Domain.Services.DTOs;

namespace Domain.Services.Interfaces;

public interface IRegisterCustomerService
{
    Task<CustomerResponse> GetByCpfAsync(string cpf, CancellationToken cancellationToken);
    Task<CustomerResponse> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<CustomerResponse> RegisterAsync(RegisterCustomerRequest request, CancellationToken cancellationToken);
}