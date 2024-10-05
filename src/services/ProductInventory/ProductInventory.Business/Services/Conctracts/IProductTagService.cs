using Common.ErrorHandling;
using ProductInventory.Business.DTOs.ProductTag;

namespace ProductInventory.Business.Services.Conctracts;

public interface IProductTagService
{
    Task<ErrorOr<int>> CreateProductTagAsync(CreateProductTagRequest request);
    Task<ErrorOr<IReadOnlyList<ProductTagDto>>> GetProductTagsAsync(GetProductTagsRequest request);
    Task<ErrorOr<ProductTagDto>> GetProductTagByIdAsync(GetProductTagByIdRequest request);
    Task<ErrorOr<bool>> DeleteProductTagAsync(DeleteProductTagRequest request);
    Task<ErrorOr<bool>> UpdateProductTagAsync(UpdateProductTagRequest request);
}