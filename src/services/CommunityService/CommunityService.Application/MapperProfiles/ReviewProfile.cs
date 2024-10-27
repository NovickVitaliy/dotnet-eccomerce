using AutoMapper;
using CommunityService.Application.DTOs.Review;
using CommunityService.Domain.Models;

namespace CommunityService.Application.MapperProfiles;

public class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        CreateMap<Review, ReviewDto>()
            .ForMember(x => x.Id,
                opt => opt.MapFrom(x => x.Id.Id));
    }
}