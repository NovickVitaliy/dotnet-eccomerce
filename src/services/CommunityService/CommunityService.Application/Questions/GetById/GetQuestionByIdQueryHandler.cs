using System.Text.Json;
using AutoMapper;
using Common.CQRS.Query;
using Common.ErrorHandling;
using CommunityService.Application.Data;
using CommunityService.Application.DTOs.Question;
using CommunityService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace CommunityService.Application.Questions.GetById;

public class GetQuestionByIdQueryHandler : IQueryHandler<GetQuestionByIdQuery, ErrorOr<QuestionDto>>
{
    private readonly IAppDbContext _db;
    private readonly IMapper _mapper;
    private readonly IDistributedCache _cache;
    
    public GetQuestionByIdQueryHandler(IAppDbContext db, IMapper mapper, IDistributedCache cache)
    {
        _db = db;
        _mapper = mapper;
        _cache = cache;
    }
    
    public async Task<ErrorOr<QuestionDto>> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
    {
        var key = $"question-{request.Id}";
        var cached = await _cache.GetStringAsync(key, token: cancellationToken);
        if (!string.IsNullOrEmpty(cached))
        {
            return JsonSerializer.Deserialize<QuestionDto>(cached)!;
        }
        
        var question = await _db.Questions
            .AsNoTracking()
            .FirstOrDefaultAsync(q => q.Id == QuestionId.Of(request.Id), cancellationToken);

        if (question is null)
        {
            return ErrorOr<QuestionDto>.NotFound();
        }
        
        var questionDto = _mapper.Map<QuestionDto>(question);
        await _cache.SetStringAsync(key, JsonSerializer.Serialize(questionDto), token: cancellationToken, options:new DistributedCacheEntryOptions()
        {
            SlidingExpiration = TimeSpan.FromMinutes(10)
        });
        return questionDto;
    }
}