using ProductInventory.Domain.Models;

namespace ProductInventory.DataAccess.Repositories.Contracts;

public interface ICategoryRepository
{
    Task<int> CreateCategoryAsync(Category category);
    Task<Category?> GetCategoryByIdAsync(int categoryId);
    Task<List<Category>> GetCategoriesAsync(int pageNumber, int pageSize);
    Task<bool> DeleteCategoryAsync(int categoryId);
    Task<bool> UpdateCategoryAsync(Category category);
}