using Adapter.Controllers.DTOs.Extensions;
using Adapter.Presenters.DTOs;
using Business.Entities;

namespace Adapter.Presenters;
public class OrderPresenter
{
    public OrderResponse ViewModel { get; init; }

    public OrderPresenter(Order order)
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

        ViewModel = order.ToResponse();
    }
}
