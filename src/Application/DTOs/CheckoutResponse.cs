namespace Application.DTOs;

public record CheckoutResponse(long PaymentId, string PaymentMethod, string QrCode, string QrCodeBase64, decimal TotalPrice) { }
