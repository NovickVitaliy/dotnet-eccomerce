using Microsoft.Extensions.Caching.Memory;
using ProductInventory.DataAccess.Constants;
using ProductInventory.DataAccess.Repositories.Contracts;

namespace ProductInventory.DataAccess.Repositories.Implementations.Supplier;

public class CachedSupplierRepository : ISupplierRepository
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly IMemoryCache _cache;
    public CachedSupplierRepository(ISupplierRepository supplierRepository, IMemoryCache cache)
    {
        _supplierRepository = supplierRepository;
        _cache = cache;
    }
    public Task<int> CreateSupplierAsync(Domain.Models.Supplier supplier)
    {
        return _supplierRepository.CreateSupplierAsync(supplier);
    }
    public async Task<Domain.Models.Supplier?> GetSupplierByIdAsync(int supplierId)
    {
        var key = $"supplier-{supplierId}";
        var suppleir = await _cache.GetOrCreateAsync(key, async entry =>
        {
            entry.AbsoluteExpiration = DateTimeOffset.Now.Add(CachingConstants.CacheEntryLifeTime);
            return await _supplierRepository.GetSupplierByIdAsync(supplierId);
        });

        return suppleir;
    }
    public async Task<List<Domain.Models.Supplier>> GetSuppliersAsync(int pageNumber, int pageSize)
    {
        var key = $"suppliers-pageNumber:{pageNumber}:pageSize:{pageSize}";
        var suppliers = await _cache.GetOrCreateAsync(key, async entry =>
        {
            entry.AbsoluteExpiration = DateTimeOffset.Now.Add(CachingConstants.CacheEntryLifeTime);
            return await _supplierRepository.GetSuppliersAsync(pageNumber, pageSize);
        });

        return suppliers;
    }
    public Task<bool> DeleteSupplierAsync(int supplierId)
    {
        return _supplierRepository.DeleteSupplierAsync(supplierId);
    }
    public Task<bool> UpdateSupplierAsync(Domain.Models.Supplier supplier)
    {
        return _supplierRepository.UpdateSupplierAsync(supplier);
    }
}