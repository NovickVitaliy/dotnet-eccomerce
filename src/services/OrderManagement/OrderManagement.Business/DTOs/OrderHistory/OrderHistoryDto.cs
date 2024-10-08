namespace OrderManagement.Business.DTOs.OrderHistory;

public record OrderHistoryDto(
    int OrderId,
    DateTime StatusChangedDate,
    string NewStatus);