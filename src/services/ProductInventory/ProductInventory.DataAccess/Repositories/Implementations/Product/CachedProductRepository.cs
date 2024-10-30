using Microsoft.Extensions.Caching.Memory;
using ProductInventory.DataAccess.Constants;
using ProductInventory.DataAccess.Repositories.Contracts;

namespace ProductInventory.DataAccess.Repositories.Implementations.Product;

public class CachedProductRepository : IProductRepository
{
    private readonly IProductRepository _productRepository;
    private readonly IMemoryCache _cache;
    
    public CachedProductRepository(IProductRepository productRepository, IMemoryCache cache)
    {
        _productRepository = productRepository;
        _cache = cache;
    }
    
    public Task<int> CreateProductAsync(Domain.Models.Product product)
    {
        return _productRepository.CreateProductAsync(product);
    }
    public async Task<Domain.Models.Product?> GetProductByIdAsync(int productId)
    {
        var key = $"product-{productId}";
        var product = await _cache.GetOrCreateAsync(key, async entry =>
        {
            entry.AbsoluteExpiration = DateTimeOffset.Now.Add(CachingConstants.CacheEntryLifeTime);
            return await _productRepository.GetProductByIdAsync(productId);
        });

        return product;
    }
    public async Task<List<Domain.Models.Product>> GetProductsAsync(int pageNumber, int pageSize)
    {
        var key = $"products-pageNumber:{pageNumber}:pageSize:{pageSize}";
        var products = await _cache.GetOrCreateAsync(key, async entry =>
        {
            entry.AbsoluteExpiration = DateTimeOffset.Now.Add(CachingConstants.CacheEntryLifeTime);
            return await _productRepository.GetProductsAsync(pageNumber, pageSize);
        });

        return products;
    }
    public Task<bool> DeleteProductAsync(int productId)
    {
        return _productRepository.DeleteProductAsync(productId);
    }
    public Task<bool> UpdateProductAsync(Domain.Models.Product product)
    {
        return _productRepository.UpdateProductAsync(product);
    }
}