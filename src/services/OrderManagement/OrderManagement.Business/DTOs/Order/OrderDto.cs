using OrderManagement.Business.DTOs.OrderHistory;
using OrderManagement.Business.DTOs.OrderItem;
using OrderManagement.Business.DTOs.Shipment;

namespace OrderManagement.Business.DTOs.Order;

public record OrderDto(
    int CustomerId,
    decimal TotalAmount,
    string OrderStatus,
    DateTime OrderDate,
    string ShippingAddress,
    IReadOnlyCollection<OrderItemDto> OrderItems,
    ShipmentDto Shipment,
    OrderHistoryDto OrderHistory);