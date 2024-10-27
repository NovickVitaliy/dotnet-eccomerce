using AutoMapper;
using Common.CQRS.Query;
using Common.ErrorHandling;
using CommunityService.Application.Data;
using CommunityService.Application.DTOs.Question;
using CommunityService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CommunityService.Application.Questions.GetById;

public class GetQuestionByIdQueryHandler : IQueryHandler<GetQuestionByIdQuery, ErrorOr<QuestionDto>>
{
    private readonly IAppDbContext _db;
    private readonly IMapper _mapper;
    
    public GetQuestionByIdQueryHandler(IAppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    
    public async Task<ErrorOr<QuestionDto>> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
    {
        var question = await _db.Questions
            .AsNoTracking()
            .FirstOrDefaultAsync(q => q.Id == QuestionId.Of(request.Id), cancellationToken);

        if (question is null)
        {
            return ErrorOr<QuestionDto>.NotFound();
        }

        var questionDto = _mapper.Map<QuestionDto>(question);
        return questionDto;
    }
}