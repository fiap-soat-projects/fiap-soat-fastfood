using Adapter.Presenters.DTOs;
using Business.Entities;
using Business.Entities.Page;

namespace Adapter.Controllers.DTOs.Extensions;
internal static class PaginationExtensions
{
    internal static Pagination<OrderResponse> ToResponse(this Pagination<Order> pagination)
    {
        return new Pagination<OrderResponse>()
        {
            Page = pagination.Page,
            Size = pagination.Size,
            TotalPages = pagination.TotalPages,
            TotalCount = pagination.TotalCount,
            Items = pagination.Items.Select(item => item.ToResponse())
        };
    }
}
