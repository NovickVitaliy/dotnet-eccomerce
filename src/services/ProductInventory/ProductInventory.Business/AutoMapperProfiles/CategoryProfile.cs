using AutoMapper;
using ProductInventory.Business.DTOs.Category;
using ProductInventory.Domain.Models;

namespace ProductInventory.Business.AutoMapperProfiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CreateCategoryRequest, Category>()
            .ForMember(x => x.CategoryId, 
                opt => opt.Ignore());
        CreateMap<Category, CategoryDto>();
        CreateMap<UpdateCategoryRequest, Category>();
    }
}