using AutoMapper;
using ProductInventory.Business.DTOs.ProductTag;
using ProductInventory.Domain.Models;

namespace ProductInventory.Business.AutoMapperProfiles;

public class ProductTagProfile : Profile
{
    public ProductTagProfile()
    {
        CreateMap<CreateProductTagRequest, ProductTag>();
        CreateMap<Product, ProductTag>();
        CreateMap<UpdateProductTagRequest, ProductTag>();
    }
}