namespace ProductInventory.Domain.Models;

public class Supplier
{
    public int Id { get; init; }
    public string Name { get; set; }
    public string ContactInfo { get; set; }
    public string Address { get; set; }
}