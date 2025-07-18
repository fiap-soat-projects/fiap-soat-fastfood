namespace Infrastructure.Entities.Extensions;

using Business.Entities;
using Business.Entities.Page;
using Infrastructure.Entities;
using Infrastructure.Entities.Page;

internal static class PaginationExtension
{
    internal static Pagination<Order> ToDomain(this PagedResult<OrderMongoDb> pagination)
    {
        return new Pagination<Order>()
        {
            Page = pagination.Page,
            Size = pagination.Size,
            TotalPages = pagination.TotalPages,
            TotalCount = pagination.TotalCount,
            Items = pagination.Items.Select(item => item.ToDomain())
        };
    }
}
