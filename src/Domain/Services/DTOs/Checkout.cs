namespace Domain.Services.DTOs;

public record Checkout(long PaymentId, string PaymentMethod, string QrCode, string QrCodeBase64, decimal TotalPrice) { }
