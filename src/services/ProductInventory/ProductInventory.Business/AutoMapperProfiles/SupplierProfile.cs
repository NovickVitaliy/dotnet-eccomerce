using AutoMapper;
using ProductInventory.Business.DTOs.Supplier;
using ProductInventory.Domain.Models;

namespace ProductInventory.Business.AutoMapperProfiles;

public class SupplierProfile : Profile
{
    public SupplierProfile()
    {
        CreateMap<CreateSupplierRequest, Supplier>();
        CreateMap<Supplier, SupplierDto>();
        CreateMap<UpdateSupplierRequest, Supplier>();
    }
}