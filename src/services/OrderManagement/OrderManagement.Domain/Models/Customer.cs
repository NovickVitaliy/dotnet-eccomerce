namespace OrderManagement.Domain.Models;

public class Customer : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public List<CustomerOrder> CustomerOrders { get; set; }
}