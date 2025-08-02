using Business.Entities.Enums;

namespace Adapter.Controllers.DTOs;
public class PaymentWebhook
{
    public string? OrderId { get; set; }    
    public string? PaymentId { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
}
