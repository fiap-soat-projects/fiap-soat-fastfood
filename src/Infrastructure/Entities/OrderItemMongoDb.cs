using Business.Entities;
using Business.Entities.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.Entities;

[BsonIgnoreExtraElements]
internal class OrderItemMongoDb
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public int Amount { get; set; }
    public ItemCategory Category { get; set; }

    public OrderItemMongoDb()
    {

    }

    internal OrderItemMongoDb(OrderItem orderItem)
    {
        Id = orderItem.Id;
        Name = orderItem.Name;
        Price = orderItem.Price;
        Amount = orderItem.Amount;
        Category = orderItem.Category;
    }

    internal OrderItem ToDomain()
    {
        return new OrderItem
        (
            Id!,
            Name!,
            Category,
            Price,
            Amount
        );
    }
}
