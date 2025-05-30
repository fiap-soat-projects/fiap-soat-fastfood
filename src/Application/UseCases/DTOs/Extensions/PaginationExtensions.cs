using Application.UseCases.DTOs.Response;
using Domain.Entities;

namespace Application.UseCases.DTOs.Extensions;
internal static class PaginationExtensions
{
    internal static Pagination<OrderGetResponse> ToResponse(this Pagination<Order> pagination)
    {
        return new Pagination<OrderGetResponse>()
        {
            Page = pagination.Page,
            Size = pagination.Size,
            TotalPages = pagination.TotalPages,
            TotalCount = pagination.TotalCount,
            Items = pagination.Items.Select(item => item.ToResponse())
        };
    }
}
