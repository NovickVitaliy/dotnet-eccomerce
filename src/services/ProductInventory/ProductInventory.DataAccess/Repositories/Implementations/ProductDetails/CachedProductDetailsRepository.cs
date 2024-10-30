using Microsoft.Extensions.Caching.Memory;
using ProductInventory.DataAccess.Constants;
using ProductInventory.DataAccess.Repositories.Contracts;
using ProductInventory.Domain.Models;

namespace ProductInventory.DataAccess.Repositories.Implementations.ProductDetails;

public class CachedProductDetailsRepository: IProductDetailsRepository
{
    private readonly IProductDetailsRepository _productDetailsRepository;
    private readonly IMemoryCache _cache;
    public CachedProductDetailsRepository(IProductDetailsRepository productDetailsRepository, IMemoryCache cache)
    {
        _productDetailsRepository = productDetailsRepository;
        _cache = cache;
    }
    
    public Task<int> CreateProductDetailAsync(ProductDetail productDetail)
    {
        return _productDetailsRepository.CreateProductDetailAsync(productDetail);
    }
    public async Task<ProductDetail?> GetProductDetailByIdAsync(int productDetailId)
    {
        var key = $"product-{productDetailId}";
        var productDetail = await _cache.GetOrCreateAsync(key, async entry =>
        {
            entry.AbsoluteExpiration = DateTimeOffset.Now.Add(CachingConstants.CacheEntryLifeTime);
            return await _productDetailsRepository.GetProductDetailByIdAsync(productDetailId);
        });

        return productDetail;
    }
    public async Task<List<ProductDetail>> GetProductDetailsAsync(int pageNumber, int pageSize)
    {
        var key = $"product-details-pageNumber:{pageNumber}:pageSize:{pageSize}";
        var productDetails = await _cache.GetOrCreateAsync(key, async entry =>
        {
            entry.AbsoluteExpiration = DateTimeOffset.Now.Add(CachingConstants.CacheEntryLifeTime);
            return await _productDetailsRepository.GetProductDetailsAsync(pageNumber, pageSize);
        });

        return productDetails;
    }
    public Task<bool> DeleteProductDetailAsync(int productDetailId)
    {
        return _productDetailsRepository.DeleteProductDetailAsync(productDetailId);
    }
    public Task<bool> UpdateProductDetailAsync(ProductDetail productDetail)
    {
        return _productDetailsRepository.UpdateProductDetailAsync(productDetail);
    }
}