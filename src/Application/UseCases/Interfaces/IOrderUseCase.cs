using Application.DTOs;
using Domain.Entities;

namespace Application.UseCases.Interfaces;
public interface IOrderUseCase
{
    Task<OrderGetResponse> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<Pagination<OrderGetResponse>> GetAllAsync(OrderFilter filter, CancellationToken cancellationToken);
    Task<string> CreateAsync(CreateRequest request, CancellationToken cancellationToken);
    Task DeleteAsync(string id, CancellationToken cancellationToken);
    Task<OrderGetResponse> UpdateStatusAsync(
        string id,
        UpdateStatusRequest updateStatusRequest,
        CancellationToken cancellationToken);

    Task<CheckoutResponse> CheckoutAsync(string id, CheckoutRequest request, CancellationToken cancellationToken);

    Task ConfirmPaymentAsync(string id, CancellationToken cancellationToken);

}
