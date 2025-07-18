using Business.Entities;
using Business.Entities.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.Entities;

[BsonIgnoreExtraElements]
[BsonDiscriminator("order")]
internal class OrderMongoDb : MongoEntity
{
    public string? CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public List<OrderItemMongoDb> Items { get; set; } = [];
    public OrderStatus Status { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public decimal TotalPrice { get; set; }

    public OrderMongoDb()
    {

    }

    internal OrderMongoDb(Order order)
    {
        CustomerId = order.CustomerId;
        CustomerName = order.CustomerName;
        Items = [.. order.Items.Select(item => new OrderItemMongoDb(item))];
        Status = order.Status;
        PaymentMethod = order.PaymentMethod;
        TotalPrice = order.TotalPrice;
    }

    internal Order ToDomain()
    {
        var items = Items.Select(item =>
        {
            return new OrderItem(
                item.Id!,
                item.Name!,
                item.Category,
                item.Price,
                item.Amount);
        });

        return new Order(
            Id,
            CustomerId,
            CustomerName,
            items,
            Status,
            PaymentMethod,
            TotalPrice);
    }
}
