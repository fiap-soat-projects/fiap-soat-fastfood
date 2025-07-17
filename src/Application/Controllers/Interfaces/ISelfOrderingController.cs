using Application.Controllers.DTOs.Request;
using Application.Controllers.DTOs.Response;

namespace Application.Controllers.Interfaces;

public interface ISelfOrderingController
{
    Task<RegisterCustomerResponse> RegisterAsync(RegisterCustomerRequest input, CancellationToken cancellationToken);
    Task<RegisterCustomerResponse> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<RegisterCustomerResponse> GetByCpfAsync(string cpf, CancellationToken cancellationToken);
}
