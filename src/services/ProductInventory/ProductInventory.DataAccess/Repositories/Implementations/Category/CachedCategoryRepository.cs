using Microsoft.Extensions.Caching.Memory;
using ProductInventory.DataAccess.Constants;
using ProductInventory.DataAccess.Repositories.Contracts;

namespace ProductInventory.DataAccess.Repositories.Implementations.Category;

public class CachedCategoryRepository : ICategoryRepository
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMemoryCache _cache;
    
    public CachedCategoryRepository(ICategoryRepository categoryRepository, IMemoryCache cache)
    {
        _categoryRepository = categoryRepository;
        _cache = cache;
    }

    public Task<int> CreateCategoryAsync(Domain.Models.Category category)
    {
        return _categoryRepository.CreateCategoryAsync(category);
    }
    public async Task<Domain.Models.Category?> GetCategoryByIdAsync(int categoryId)
    {
        var key = $"category-{categoryId}";
        if (_cache.TryGetValue(key, out Domain.Models.Category? category))
        {
            return category;
        }

        category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
        _cache.Set(key, category, DateTimeOffset.Now.Add(CachingConstants.CacheEntryLifeTime));

        return category;
    }
    public async Task<List<Domain.Models.Category>> GetCategoriesAsync(int pageNumber, int pageSize)
    {
        var key = $"categories-pageNumber:{pageNumber}:pageSize:{pageSize}";
        var categories = await _cache.GetOrCreateAsync(
            key, 
            async entry =>
            {
                entry.AbsoluteExpiration = DateTimeOffset.Now.Add(CachingConstants.CacheEntryLifeTime);
                return await _categoryRepository
                    .GetCategoriesAsync(pageNumber, pageSize);
            });

        return categories;
    }
    public Task<bool> DeleteCategoryAsync(int categoryId)
    {
        return _categoryRepository.DeleteCategoryAsync(categoryId);
    }
    public Task<bool> UpdateCategoryAsync(Domain.Models.Category category)
    {
        return _categoryRepository.UpdateCategoryAsync(category);
    }
}