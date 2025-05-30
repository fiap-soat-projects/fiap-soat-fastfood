using Domain.Entities.Enums;
using Infrastructure.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Domain.Entities;

namespace Infrastructure.Repositories.Entities;

[BsonIgnoreExtraElements]
[BsonDiscriminator("order")]
internal class OrderMongoDb : MongoEntity
{
    public string? CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public List<OrderItemMongoDb> Items { get; set; } = [];

    [BsonRepresentation(BsonType.String)]
    internal OrderStatus Status { get; set; }

    [BsonRepresentation(BsonType.String)]
    internal PaymentMethod PaymentMethod { get; set; }

    public decimal TotalPrice { get; set; }

    public OrderMongoDb()
    {
        
    }

    internal OrderMongoDb(Order order)
    {
        CustomerId = order.CustomerId;
        CustomerName = order.CustomerName;
        Items = order.Items.Select(item => new OrderItemMongoDb(item)).ToList();
        Status = order.Status;
        PaymentMethod = order.PaymentMethod;
        TotalPrice = order.TotalPrice;
    }
}
