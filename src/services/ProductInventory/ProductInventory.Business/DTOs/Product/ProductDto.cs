using ProductInventory.Business.DTOs.Category;
using ProductInventory.Business.DTOs.ProductDetail;
using ProductInventory.Business.DTOs.Supplier;

namespace ProductInventory.Business.DTOs.Product;

public record ProductDto(
    string Name,
    string Description,
    decimal Price,
    int StockQuantity,
    int CategoryId,
    int SupplierId,
    CategoryDto Category,
    SupplierDto Supplier,
    ProductDetailDto ProductDetail);