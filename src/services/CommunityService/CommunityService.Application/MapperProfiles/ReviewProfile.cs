using AutoMapper;
using CommunityService.Application.DTOs.Review;
using CommunityService.Domain.Models;
using CommunityService.Domain.ValueObjects;

namespace CommunityService.Application.MapperProfiles;

public class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        CreateMap<ReviewId, Guid>()
            .ConvertUsing(src => src.Id);
        CreateMap<Review, ReviewDto>()
            .ForMember(x => x.Id,
                opt => opt.MapFrom(x => x.Id.Id));
    }
}