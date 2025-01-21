using AutoMapper;
using CommunityService.Application.DTOs.Question;
using CommunityService.Domain.Models;
using CommunityService.Domain.ValueObjects;

namespace CommunityService.Application.MapperProfiles;

public class QuestionProfile : Profile
{
    public QuestionProfile()
    {
        CreateMap<QuestionId, Guid>()
            .ConvertUsing(src => src.Id);

        CreateMap<Question, QuestionDto>()
            .ForMember(x => x.Id,
                opt => opt.MapFrom(x => x.Id.Id));
    }
}