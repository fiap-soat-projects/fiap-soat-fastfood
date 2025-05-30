using Domain.Entities;

namespace Domain.Services.DTOs.Extensions;
internal static class PaginationExtensions
{
    internal static Pagination<GetResponse> ToResponse(this Pagination<Order> pagination)
    {
        return new Pagination<GetResponse>()
        {
            Page = pagination.Page,
            Size = pagination.Size,
            TotalPages = pagination.TotalPages,
            TotalCount = pagination.TotalCount,
            Items = pagination.Items.Select(item => item.ToResponse())
        };
    }
}
