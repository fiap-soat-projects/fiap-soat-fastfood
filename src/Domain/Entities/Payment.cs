using Business.Entities.Enums;

namespace Business.Entities;
public class Payment
{
    public string? Id { get; set; }
    public PaymentMethod Method { get; set; }
    public PaymentStatus Status { get; set; }
}
