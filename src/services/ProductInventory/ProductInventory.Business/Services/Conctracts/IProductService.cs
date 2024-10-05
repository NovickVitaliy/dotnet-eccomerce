using Common.ErrorHandling;
using ProductInventory.Business.DTOs.Product;

namespace ProductInventory.Business.Services.Conctracts;

public interface IProductService
{
    Task<ErrorOr<int>> CreateProductAsync(CreateProductRequest request);
    Task<ErrorOr<IReadOnlyList<ProductDto>>> GetProductsAsync(GetProductsRequest request);
    Task<ErrorOr<ProductDto>> GetProductByIdAsync(GetProductByIdRequest request);
    Task<ErrorOr<bool>> DeleteProductAsync(DeleteProductRequest request);
    Task<ErrorOr<bool>> UpdateProductAsync(UpdateProductRequest request);
}