using AutoMapper;
using ProductInventory.Business.DTOs.ProductDetail;
using ProductInventory.Domain.Models;

namespace ProductInventory.Business.AutoMapperProfiles;

public class ProductDetailProfile : Profile
{
    public ProductDetailProfile()
    {
        CreateMap<CreateProductDetailRequest, ProductDetail>();
        CreateMap<ProductDetail, ProductDetailDto>();
        CreateMap<UpdateProductDetailRequest, ProductDetail>();
    }
}