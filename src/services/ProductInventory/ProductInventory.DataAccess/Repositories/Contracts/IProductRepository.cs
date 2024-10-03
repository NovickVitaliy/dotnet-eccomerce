using ProductInventory.Domain.Models;

namespace ProductInventory.DataAccess.Repositories.Contracts;

public interface IProductRepository
{
    Task<int> CreateProductAsync(Product product);
    Task<Product?> GetProductByIdAsync(int productId);
    Task<List<Product>> GetProductsAsync(int pageNumber, int pageSize);
    Task<bool> DeleteProductAsync(int productId);
    Task<bool> UpdateProductAsync(Product product);
    
}