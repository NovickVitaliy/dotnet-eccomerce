namespace OrderManagement.Business.DTOs.Shipment;

public record ShipmentDto(
    int OrderId,
    DateTime EstimatedArrival,
    int TrackingNumber,
    DateTime ShippingDate);