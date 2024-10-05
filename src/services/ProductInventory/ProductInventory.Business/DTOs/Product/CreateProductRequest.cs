namespace ProductInventory.Business.DTOs.Product;

public record CreateProductRequest(
    string Name,
    string Description,
    decimal Price,
    int StockQuantity,
    int CategoryId,
    int SupplierId);