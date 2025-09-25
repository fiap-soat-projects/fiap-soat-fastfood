using Adapter.Controllers.DTOs;
using Adapter.Presenters;
using Adapter.Presenters.DTOs;

namespace Adapter.Controllers.Interfaces;

public interface ISelfOrderingController
{
    Task<CustomerPresenter> RegisterAsync(RegisterCustomerRequest input, CancellationToken cancellationToken);
    Task<CustomerPresenter> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<CustomerPresenter> GetByCpfAsync(string cpf, CancellationToken cancellationToken);
}
