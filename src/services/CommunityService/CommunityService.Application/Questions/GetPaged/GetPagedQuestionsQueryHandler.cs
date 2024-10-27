using AutoMapper;
using Common.CQRS.Query;
using Common.ErrorHandling;
using CommunityService.Application.Data;
using CommunityService.Application.DTOs.Question;
using Microsoft.EntityFrameworkCore;

namespace CommunityService.Application.Questions.GetPaged;

public class GetPagedQuestionsQueryHandler : IQueryHandler<GetPagedQuestionQuery, ErrorOr<IReadOnlyCollection<QuestionDto>>>
{
    private readonly IAppDbContext _db;
    private readonly IMapper _mapper;
    
    public GetPagedQuestionsQueryHandler(IAppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    
    public async Task<ErrorOr<IReadOnlyCollection<QuestionDto>>> Handle(GetPagedQuestionQuery request, CancellationToken cancellationToken)
    {
        var skip = (request.PageNumber - 1) * request.PageSize;
        var take = request.PageSize;

        var questions = await _db.Questions
            .AsNoTracking()
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken: cancellationToken);

        return questions.Select(q => _mapper.Map<QuestionDto>(q)).ToList();
    }
}