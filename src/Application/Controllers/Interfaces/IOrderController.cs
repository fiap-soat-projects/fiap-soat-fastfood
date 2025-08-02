using Adapter.Controllers.DTOs;
using Adapter.Controllers.DTOs.Filters;
using Adapter.Presenters;
using Business.Entities.Page;

namespace Adapter.Controllers.Interfaces;

public interface IOrderController
{
    Task<string> CreateAsync(CreateRequest request, CancellationToken cancellationToken);
    Task<OrderGetResponse> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<Pagination<OrderGetResponse>> GetAllAsync(OrderFilter filter, CancellationToken cancellationToken);
    Task<Pagination<OrderGetResponse>> GetActiveAsync(OrderFilter filter, CancellationToken cancellationToken);
    Task<CheckoutResponse> CheckoutAsync(string id, CheckoutRequest request, CancellationToken cancellationToken);
    Task ConfirmPaymentAsync(string id, CancellationToken cancellationToken);
    Task ProcessPaymentAsync(PaymentWebhook request, CancellationToken cancellationToken);
    Task<OrderGetResponse> UpdateStatusAsync(string id, UpdateStatusRequest updateStatusRequest, CancellationToken cancellationToken);
    Task DeleteAsync(string id, CancellationToken cancellationToken);
}
