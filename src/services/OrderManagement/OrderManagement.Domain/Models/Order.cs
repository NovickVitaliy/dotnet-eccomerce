namespace OrderManagement.Domain.Models;

public class Order : BaseEntity
{
    public int UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public string OrderStatus { get; set; }
    public DateTime OrderDate { get; set; }
    public string ShippingAddress { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
    public Shipment Shipment { get; set; }
    public OrderHistory OrderHistory { get; set; }
    public List<CustomerOrder> CustomerOrders { get; set; }
}