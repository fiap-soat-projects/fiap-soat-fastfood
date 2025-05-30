namespace Infrastructure.Repositories.Entities.Extensions;

using Domain.Entities;
using Domain.Entities.Page;
using Infrastructure.Entities;
using Infrastructure.Repositories.Entities;

internal static class PaginationExtension
{
    internal static Pagination<Order> ToEntity(this PagedResult<OrderMongoDb> pagination)
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
