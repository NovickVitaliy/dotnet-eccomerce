using ProductInventory.Domain.Models;

namespace ProductInventory.DataAccess.Repositories.Contracts;

public interface IProductDetailsRepository
{
    Task<int> CreateProductDetailAsync(ProductDetail productDetail);
    Task<ProductDetail?> GetProductDetailByIdAsync(int productDetailId);
    Task<List<ProductDetail>> GetProductDetailsAsync(int pageNumber, int pageSize);
    Task<bool> DeleteProductDetailAsync(int productDetailId);
    Task<bool> UpdateProductDetailAsync(ProductDetail productDetail);
}