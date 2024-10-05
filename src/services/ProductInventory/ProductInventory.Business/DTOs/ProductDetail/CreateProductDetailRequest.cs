namespace ProductInventory.Business.DTOs.ProductDetail;

public record CreateProductDetailRequest(
    int ProductDetailId,
    int Weight,
    int Height,
    int Width,
    int Length,
    string Color,
    TimeSpan? WarrantyPeriod);