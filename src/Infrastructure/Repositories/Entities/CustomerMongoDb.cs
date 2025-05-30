using Domain.Entities;
using Infrastructure.Entities;
using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.Repositories.Entities;

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

    public CustomerMongoDb(Customer customer)
    {
        Name = customer.Name;
        Cpf = customer.Cpf;
        Email = customer.Email;
    }

    internal Customer ToEntity()
    {
        return new Customer(Id, CreatedAt)
        {
            Name = Name!,
            Cpf = Cpf!,
            Email = Email!
        };
    }
}
