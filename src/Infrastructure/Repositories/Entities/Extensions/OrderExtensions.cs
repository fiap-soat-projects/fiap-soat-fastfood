using Domain.Entities;

namespace Infrastructure.Repositories.Entities.Extensions;
internal static class OrderExtensions
{
    internal static Order ToDomain(this OrderMongoDb orderMongoDb)
    {
        return new Order(
            orderMongoDb.Id,
            orderMongoDb.CustomerId,
            orderMongoDb.CustomerName,
            orderMongoDb.Items.Select(item => new OrderItem(item.Id, item.Name, item.Category, item.Price, item.Amount)),
            orderMongoDb.Status,
            orderMongoDb.PaymentMethod,
            orderMongoDb.TotalPrice);
    }
}