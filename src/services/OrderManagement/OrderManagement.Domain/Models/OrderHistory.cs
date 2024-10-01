namespace OrderManagement.Domain.Models;

public class OrderHistory
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public DateTime StatusChangedDate { get; set; }
    public string NewStatus { get; set; }
}