using Adapter.Presenters.DTOs;
using Business.Entities;

namespace Adapter.Presenters;
public class CustomerPresenter
{
    public RegisterCustomerResponse ViewModel { get; init; }

    public CustomerPresenter(Customer customer)
    {
        ViewModel = new RegisterCustomerResponse
        {
            Id = customer.Id,
            CreatedAt = customer.CreatedAt,
            Name = customer.Name,
            Cpf = customer.Cpf,
            Email = customer.Email
        };
    }
}
