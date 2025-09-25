using Adapter.Controllers.DTOs;
using Adapter.Controllers.Interfaces;
using Adapter.Presenters;
using Adapter.Presenters.DTOs;
using Business.Entities;
using Business.UseCases.Interfaces;

namespace Adapter.Controllers;

internal class SelfOrderingController : ISelfOrderingController
{
    private readonly ICustomerUseCase _customerUseCase;

    public SelfOrderingController(ICustomerUseCase customerUseCase)
    {
        _customerUseCase = customerUseCase;
    }

    public async Task<CustomerPresenter> RegisterAsync(RegisterCustomerRequest input, CancellationToken cancellationToken)
    {
        var customer = new Customer(input.Name, input.Cpf, input.Email);

        customer = await _customerUseCase.CreateAsync(customer, cancellationToken);

        return new CustomerPresenter(customer);
    }

    public async Task<CustomerPresenter> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var customer = await _customerUseCase.GetByIdAsync(id, cancellationToken);

        return new CustomerPresenter(customer);
    }

    public async Task<CustomerPresenter> GetByCpfAsync(string cpf, CancellationToken cancellationToken)
    {
        var customer = await _customerUseCase.GetByCpfAsync(cpf, cancellationToken);

        return new CustomerPresenter(customer);
    }
}
