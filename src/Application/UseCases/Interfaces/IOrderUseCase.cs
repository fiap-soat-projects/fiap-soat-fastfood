using Application.UseCases.DTOs.Filters;
using Application.UseCases.DTOs.Request;
using Application.UseCases.DTOs.Response;
using Domain.Entities.Page;

namespace Application.UseCases.Interfaces;

public interface IOrderUseCase
{
    Task<string> CreateAsync(CreateRequest request, CancellationToken cancellationToken);
    Task<OrderGetResponse> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<Pagination<OrderGetResponse>> GetAllAsync(OrderFilter filter, CancellationToken cancellationToken);
    Task<CheckoutResponse> CheckoutAsync(string id, CheckoutRequest request, CancellationToken cancellationToken);
    Task ConfirmPaymentAsync(string id, CancellationToken cancellationToken);
    Task<OrderGetResponse> UpdateStatusAsync(string id, UpdateStatusRequest updateStatusRequest, CancellationToken cancellationToken);
    Task DeleteAsync(string id, CancellationToken cancellationToken);
}
