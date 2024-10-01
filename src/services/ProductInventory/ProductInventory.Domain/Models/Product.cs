namespace ProductInventory.Domain.Models;

public class Product
{
    public int Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public int CategoryId { get; set; }
    public int SupplierId { get; set; }
}