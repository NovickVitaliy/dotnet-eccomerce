namespace OrderManagement.Domain.Models;

public class Shipment : BaseEntity
{
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public DateTime EstimatedArrival { get; set; }
    public Guid TrackingNumber { get; set; }
    public DateTime ShippingDate { get; set; }
}