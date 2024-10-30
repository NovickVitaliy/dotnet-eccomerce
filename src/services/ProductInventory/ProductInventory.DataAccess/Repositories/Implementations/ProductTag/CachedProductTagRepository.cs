using Microsoft.Extensions.Caching.Memory;
using ProductInventory.DataAccess.Constants;
using ProductInventory.DataAccess.Repositories.Contracts;

namespace ProductInventory.DataAccess.Repositories.Implementations.ProductTag;

public class CachedProductTagRepository : IProductTagRepository
{
    private readonly IProductTagRepository _productTagRepository;
    private readonly IMemoryCache _cache;
    public CachedProductTagRepository(IProductTagRepository productTagRepository, IMemoryCache cache)
    {
        _productTagRepository = productTagRepository;
        _cache = cache;
    }
    public Task<int> CreateProductTagAsync(Domain.Models.ProductTag productTag)
    {
        return _productTagRepository.CreateProductTagAsync(productTag);
    }
    public async Task<Domain.Models.ProductTag?> GetProductTagByIdAsync(int productTagId)
    {
        var key = $"product-tag-{productTagId}";
        var productTag = await _cache.GetOrCreateAsync(key, async entry =>
        {
            entry.AbsoluteExpiration = DateTimeOffset.Now.Add(CachingConstants.CacheEntryLifeTime);
            return await _productTagRepository.GetProductTagByIdAsync(productTagId);
        });

        return productTag;
    }
    public async Task<List<Domain.Models.ProductTag>> GetProductTagsAsync(int pageNumber, int pageSize)
    {
        var key = $"product-tags-pageNumber:{pageNumber}:pageSize:{pageSize}";
        var productTags = await _cache.GetOrCreateAsync(key, async entry =>
        {
            entry.AbsoluteExpiration = DateTimeOffset.Now.Add(CachingConstants.CacheEntryLifeTime);
            return await _productTagRepository.GetProductTagsAsync(pageNumber, pageSize);
        });

        return productTags;
    }
    public Task<bool> DeleteProductTagAsync(int productTagId)
    {
        return _productTagRepository.DeleteProductTagAsync(productTagId);
    }
    public Task<bool> UpdateProductTagAsync(Domain.Models.ProductTag productTag)
    {
        return _productTagRepository.UpdateProductTagAsync(productTag);
    }
}