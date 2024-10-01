namespace OrderManagement.Domain.Models;

public class Shipment
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public DateTime EstimatedArrival { get; set; }
    public int TrackingNumber { get; set; }
    public DateTime ShippingDate { get; set; }
}