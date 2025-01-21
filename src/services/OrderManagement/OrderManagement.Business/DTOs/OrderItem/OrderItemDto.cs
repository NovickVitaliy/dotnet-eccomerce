namespace OrderManagement.Business.DTOs.OrderItem;

public record OrderItemDto(
    int ProductId,
    int Quantity,
    decimal Price);