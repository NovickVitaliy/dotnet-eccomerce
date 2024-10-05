namespace ProductInventory.Business.DTOs.Supplier;

public record UpdateSupplierRequest(int SupplierId, string Name, string ContactInfo, string Address);