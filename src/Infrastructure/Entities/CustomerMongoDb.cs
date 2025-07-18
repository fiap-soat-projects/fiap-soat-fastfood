using Business.Entities;
using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.Entities;

[BsonIgnoreExtraElements]
[BsonDiscriminator("customer")]
internal class CustomerMongoDb : MongoEntity
{
    public string? Name { get; set; }
    public string? Cpf { get; set; }
    public string? Email { get; set; }

    public CustomerMongoDb()
    {

    }

    internal CustomerMongoDb(Customer customer)
    {
        Name = customer.Name;
        Cpf = customer.Cpf;
        Email = customer.Email;
    }

    internal Customer ToDomain()
    {
        return new Customer(Id, CreatedAt)
        {
            Name = Name!,
            Cpf = Cpf!,
            Email = Email!
        };
    }
}
