using Application.Controllers.DTOs.Filters;
using Application.Controllers.DTOs.Request;
using Application.Controllers.DTOs.Response;
using Domain.Entities.Page;

namespace Application.Controllers.Interfaces;

public interface IOrderController
{
    Task<string> CreateAsync(CreateRequest request, CancellationToken cancellationToken);
    Task<OrderGetResponse> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<Pagination<OrderGetResponse>> GetAllAsync(OrderFilter filter, CancellationToken cancellationToken);
    Task<CheckoutResponse> CheckoutAsync(string id, CheckoutRequest request, CancellationToken cancellationToken);
    Task ConfirmPaymentAsync(string id, CancellationToken cancellationToken);
    Task<OrderGetResponse> UpdateStatusAsync(string id, UpdateStatusRequest updateStatusRequest, CancellationToken cancellationToken);
    Task DeleteAsync(string id, CancellationToken cancellationToken);
}
