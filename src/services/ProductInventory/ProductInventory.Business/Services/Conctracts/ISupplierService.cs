using Common.ErrorHandling;
using ProductInventory.Business.DTOs.Supplier;

namespace ProductInventory.Business.Services.Conctracts;

public interface ISupplierService
{
    Task<ErrorOr<int>> CreateSupplierAsync(CreateSupplierRequest request);
    Task<ErrorOr<IReadOnlyList<SupplierDto>>> GetSuppliersAsync(GetSuppliersRequest reqeust);
    Task<ErrorOr<SupplierDto>> GetSupplierByIdAsync(GetSupplierByIdRequest request);
    Task<ErrorOr<bool>> DeleteSupplierAsync(DeleteSupplierRequest request);
    Task<ErrorOr<bool>> UpdateSupplierAsync(UpdateSupplierRequest request);
}