using Common.ErrorHandling;
using ProductInventory.Business.DTOs.ProductDetail;

namespace ProductInventory.Business.Services.Conctracts;

public interface IProductDetailService
{
    Task<ErrorOr<int>> CreateProductDetailAsync(CreateProductDetailRequest request);
    Task<ErrorOr<IReadOnlyList<ProductDetailDto>>> GetProductDetailsAsync(GetProductDetailsRequest request);
    Task<ErrorOr<ProductDetailDto>> GetProductDetailsByIdAsync(GetProductDetailByIdRequest request);
    Task<ErrorOr<bool>> DeleteProductDetailAsync(DeleteProductDetailRequest request);
    Task<ErrorOr<bool>> UpdateProductDetailAsync(UpdateProductDetailRequest request);
}