namespace OrderManagement.Business.DTOs.Order;

public record ChangeOrderStatusRequest(int OrderId, string NewStatus);