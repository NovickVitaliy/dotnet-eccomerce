using Common.ErrorHandling;
using ProductInventory.Business.DTOs.Category;

namespace ProductInventory.Business.Services.Conctracts;

public interface ICategoryService
{
    Task<ErrorOr<int>> CreateCategoryAsync(CreateCategoryRequest request);
    Task<ErrorOr<IReadOnlyList<CategoryDto>>> GetCategoriesAsync(GetCategoriesRequest request);
    Task<ErrorOr<CategoryDto>> GetCategoryByIdAsync(GetCategoryByIdRequest request);
    Task<ErrorOr<bool>> DeleteCategoryAsync(DeleteCategoryRequest request);
    Task<ErrorOr<bool>> UpdateCategoryAsync(UpdateCategoryRequest request);
}