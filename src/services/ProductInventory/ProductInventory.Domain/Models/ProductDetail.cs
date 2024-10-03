namespace ProductInventory.Domain.Models;

public class ProductDetail
{
    public int ProductDetailId { get; init; }
    public int Weight { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
    public int Length { get; set; }
    public string Color { get; set; }
    public TimeSpan WarrantyPeriod { get; set; }
}