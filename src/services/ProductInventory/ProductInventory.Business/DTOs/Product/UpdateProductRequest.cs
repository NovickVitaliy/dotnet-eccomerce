namespace ProductInventory.Business.DTOs.Product;

public record UpdateProductRequest(
    int ProductId,
    string Name,
    string Description,
    decimal Price,
    int StockQuantity,
    int CategoryId,
    int SupplierId);