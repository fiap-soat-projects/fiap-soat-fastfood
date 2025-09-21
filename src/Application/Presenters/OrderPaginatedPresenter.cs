using Adapter.Presenters.DTOs;
using Business.Entities;
using Business.Entities.Page;

namespace Adapter.Presenters;
public class OrderPaginatedPresenter
{
    public Pagination<OrderResponse> ViewModel { get; init; }

    public OrderPaginatedPresenter(Pagination<Order> orderPagination)
    {
        ViewModel = new Pagination<OrderResponse>()
        {
            Page = orderPagination.Page,
            Size = orderPagination.Size,
            TotalPages = orderPagination.TotalPages,
            TotalCount = orderPagination.TotalCount,
            Items = orderPagination.Items.Select(ToResponse)
        };
    }

    private static OrderResponse ToResponse(Order order)
    {
        var id = order.Id ?? string.Empty;
        var customerId = order.CustomerId ?? string.Empty;
        var customerName = order.CustomerName ?? string.Empty;

        var items = order.Items.Select(item => new OrderItemResponse(
            item.Name ?? string.Empty,
            item.Category.ToString(),
            item.Price,
            item.Amount
        ));

        var status = order.Status.ToString();
        var paymentMethod = order.Payment;
        var totalPrice = order.TotalPrice;

        return new OrderResponse(
            id,
            customerId,
            customerName,
            items,
            status,
            paymentMethod,
            totalPrice);
    }
}
