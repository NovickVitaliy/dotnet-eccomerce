namespace ProductInventory.Domain.Models;

public class Category
{
    public int CategoryId { get; init; }
    public string Name { get; set; }
    public string Description { get; set; }
}