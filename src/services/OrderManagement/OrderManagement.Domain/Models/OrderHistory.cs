namespace OrderManagement.Domain.Models;

public class OrderHistory : BaseEntity
{
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public DateTime StatusChangedDate { get; set; }
    public string NewStatus { get; set; }
}