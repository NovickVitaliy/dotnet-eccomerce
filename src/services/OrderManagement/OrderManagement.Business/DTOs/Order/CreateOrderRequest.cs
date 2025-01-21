using OrderManagement.Business.DTOs.OrderItem;

namespace OrderManagement.Business.DTOs.Order;

public record CreateOrderRequest(
    int CustomerId,
    string ShippingAddress,
    IReadOnlyCollection<OrderItemDto> OrderItems);