namespace ProductInventory.Domain.Models;

public class Product
{
    public int ProductId { get; init; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    public int SupplierId { get; set; }
    public Supplier? Supplier { get; set; }
    public ProductDetail? ProductDetail { get; set; }
    public List<ProductTag> ProductTags { get; set; }
}