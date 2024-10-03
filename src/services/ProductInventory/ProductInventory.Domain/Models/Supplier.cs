namespace ProductInventory.Domain.Models;

public class Supplier
{
    public int SupplierId { get; init; }
    public string Name { get; set; }
    public string ContactInfo { get; set; }
    public string Address { get; set; }
    public List<Product> Products { get; set; }
}