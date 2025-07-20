using Business.Entities;
using Business.Entities.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.Entities;

[BsonIgnoreExtraElements]
[BsonDiscriminator("menu")]
internal class MenuItemMongoDb : MongoEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
    public ItemCategory Category { get; set; }

    public MenuItemMongoDb()
    {

    }

    internal MenuItemMongoDb(MenuItem menuItem)
    {
        Id = menuItem.Id;
        Name = menuItem.Name;
        Description = menuItem.Description;
        Price = menuItem.Price;
        IsActive = menuItem.IsActive;
        Category = menuItem.Category;
    }

    internal MenuItem ToDomain()
    {
        return new MenuItem(Name!, Price, Description!, Category)
        {
            Id = Id,
            CreatedAt = CreatedAt,
            IsActive = IsActive
        };
    }
}
