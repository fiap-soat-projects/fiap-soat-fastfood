using Business.Entities;
using Business.Entities.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.Entities;

[BsonIgnoreExtraElements]
public class PaymentMongoDb
{
    public string? Id { get; set; }
    public PaymentMethod Method { get; set; }
    public PaymentStatus Status { get; set; }

    public PaymentMongoDb(Payment payment)
    {
        Id = payment.Id;
        Method = payment.Method;
        Status = payment.Status;
    }
}
