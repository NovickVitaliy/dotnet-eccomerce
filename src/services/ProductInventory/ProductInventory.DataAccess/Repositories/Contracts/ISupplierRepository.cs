using ProductInventory.Domain.Models;

namespace ProductInventory.DataAccess.Repositories.Contracts;

public interface ISupplierRepository
{
    Task<int> CreateSupplierAsync(Supplier supplier);
    Task<Supplier?> GetSupplierByIdAsync(int supplierId);
    Task<List<Supplier>> GetSuppliersAsync(int pageNumber, int pageSize);
    Task<bool> DeleteSupplierAsync(int supplierId);
    Task<bool> UpdateSupplierAsync(Supplier supplier);
}