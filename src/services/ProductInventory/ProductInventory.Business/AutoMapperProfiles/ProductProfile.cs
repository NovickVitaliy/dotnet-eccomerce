using AutoMapper;
using ProductInventory.Business.DTOs.Product;
using ProductInventory.Domain.Models;

namespace ProductInventory.Business.AutoMapperProfiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<CreateProductRequest, Product>();
        CreateMap<Product, ProductDto>();
        CreateMap<UpdateProductRequest, Product>();
    }
}