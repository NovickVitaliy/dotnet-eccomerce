using ProductInventory.Domain.Models;

namespace ProductInventory.DataAccess.Repositories.Contracts;

public interface IProductTagRepository
{
    Task<int> CreateProductTagAsync(ProductTag productTag);
    Task<ProductTag?> GetProductTagByIdAsync(int productTagId);
    Task<List<ProductTag>> GetProductTagsAsync(int pageNumber, int pageSize);
    Task<bool> DeleteProductTagAsync(int productTagId);
    Task<bool> UpdateProductTagAsync(ProductTag productTag);
}