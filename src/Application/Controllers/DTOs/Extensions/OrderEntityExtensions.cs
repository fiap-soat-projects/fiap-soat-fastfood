using Adapter.Presenters;
using Business.Entities;

namespace Adapter.Controllers.DTOs.Extensions;
internal static class OrderEntityExtensions
{
    internal static OrderGetResponse ToResponse(this Order order)
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

        return new OrderGetResponse(id, customerId, customerName, items, status, paymentMethod, totalPrice);
    }
}
