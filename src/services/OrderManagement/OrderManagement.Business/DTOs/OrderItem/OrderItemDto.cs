namespace OrderManagement.Business.DTOs.OrderItem;

public record OrderItemDto(
    int ProductId,
    int Quantity,
    int Price);