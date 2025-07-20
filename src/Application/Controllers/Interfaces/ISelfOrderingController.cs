using Adapter.Controllers.DTOs;
using Adapter.Presenters;

namespace Adapter.Controllers.Interfaces;

public interface ISelfOrderingController
{
    Task<RegisterCustomerResponse> RegisterAsync(RegisterCustomerRequest input, CancellationToken cancellationToken);
    Task<RegisterCustomerResponse> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<RegisterCustomerResponse> GetByCpfAsync(string cpf, CancellationToken cancellationToken);
}
