using AutoMapper;
using CommunityService.Application.DTOs.Question;
using CommunityService.Domain.Models;

namespace CommunityService.Application.MapperProfiles;

public class QuestionProfile : Profile
{
    public QuestionProfile()
    {
        CreateMap<Question, QuestionDto>()
            .ForMember(x => x.Id,
                opt => opt.MapFrom(x => x.Id.Id));
    }
}