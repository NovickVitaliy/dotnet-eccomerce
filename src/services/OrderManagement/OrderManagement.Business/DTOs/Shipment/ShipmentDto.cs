namespace OrderManagement.Business.DTOs.Shipment;

public record ShipmentDto(
    int OrderId,
    DateTime EstimatedArrival,
    Guid TrackingNumber,
    DateTime ShippingDate);