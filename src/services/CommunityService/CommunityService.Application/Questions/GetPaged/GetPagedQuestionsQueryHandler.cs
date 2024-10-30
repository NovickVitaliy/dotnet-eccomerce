using System.Text.Json;
using AutoMapper;
using Common.CQRS.Query;
using Common.ErrorHandling;
using CommunityService.Application.Data;
using CommunityService.Application.DTOs.Question;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace CommunityService.Application.Questions.GetPaged;

public class GetPagedQuestionsQueryHandler : IQueryHandler<GetPagedQuestionQuery, ErrorOr<IReadOnlyCollection<QuestionDto>>>
{
    private readonly IAppDbContext _db;
    private readonly IMapper _mapper;
    private readonly IDistributedCache _cache;
    public GetPagedQuestionsQueryHandler(IAppDbContext db, IMapper mapper, IDistributedCache cache)
    {
        _db = db;
        _mapper = mapper;
        _cache = cache;
    }
    
    public async Task<ErrorOr<IReadOnlyCollection<QuestionDto>>> Handle(GetPagedQuestionQuery request, CancellationToken cancellationToken)
    {
        var key = $"questions-pageNumber:{request.PageNumber}:pageSize:{request.PageSize}";
        var cached = await _cache.GetStringAsync(key, cancellationToken);
        if (!string.IsNullOrEmpty(cached))
        {
            return JsonSerializer.Deserialize<List<QuestionDto>>(cached)!.AsReadOnly();
        }
        var skip = (request.PageNumber - 1) * request.PageSize;
        var take = request.PageSize;

        var questions = await _db.Questions
            .AsNoTracking()
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken: cancellationToken);

        var dtos = questions.Select(q => _mapper.Map<QuestionDto>(q)).ToList().AsReadOnly();
        await _cache.SetStringAsync(key, JsonSerializer.Serialize(dtos), token: cancellationToken, options: new DistributedCacheEntryOptions()
        {
            SlidingExpiration = TimeSpan.FromMinutes(10)
        });
        return dtos;
    }
}