namespace OrderManagement.Domain.Models;

public class CustomerOrder
{
    public int CustomerId { get; init; }
    public Customer? Customer { get; set; }
    
    public int OrderId { get; init; }
    public Order? Order { get; set; }
}