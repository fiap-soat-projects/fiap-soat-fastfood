using Application.UseCases.DTOs.Request;
using Application.UseCases.DTOs.Response;

namespace Application.UseCases.Interfaces;

public interface ISelfOrderingUseCase
{
    Task<RegisterCustomerResponse> RegisterAsync(RegisterCustomerRequest input, CancellationToken cancellationToken);
    Task<RegisterCustomerResponse> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<RegisterCustomerResponse> GetByCpfAsync(string cpf, CancellationToken cancellationToken);
}
