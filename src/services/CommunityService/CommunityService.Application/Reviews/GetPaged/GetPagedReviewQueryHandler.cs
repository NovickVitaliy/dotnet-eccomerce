using System.Text.Json;
using AutoMapper;
using Common.CQRS.Query;
using Common.ErrorHandling;
using CommunityService.Application.Data;
using CommunityService.Application.DTOs.Review;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace CommunityService.Application.Reviews.GetPaged;

public class GetPagedReviewQueryHandler : IQueryHandler<GetPagedReviewQuery, ErrorOr<IReadOnlyCollection<ReviewDto>>>
{
    private readonly IAppDbContext _db;
    private readonly IMapper _mapper;
    private readonly IDistributedCache _cache;
    public GetPagedReviewQueryHandler(IAppDbContext db, IMapper mapper, IDistributedCache cache)
    {
        _db = db;
        _mapper = mapper;
        _cache = cache;
    }
    
    public async Task<ErrorOr<IReadOnlyCollection<ReviewDto>>> Handle(GetPagedReviewQuery request, CancellationToken cancellationToken)
    {
        var key = $"reviews-pageNumber:{request.PageNumber}:pageSize:{request.PageSize}";
        var cached = await _cache.GetStringAsync(key, cancellationToken);
        if (!string.IsNullOrEmpty(cached))
        {
            return JsonSerializer.Deserialize<List<ReviewDto>>(cached)!.AsReadOnly();
        }
        
        var skip = (request.PageNumber - 1) * request.PageSize;
        var take = request.PageSize;

        var reviews = await _db.Reviews.AsNoTracking()
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken: cancellationToken);

        var dtos = reviews.Select(r => _mapper.Map<ReviewDto>(r)).ToList().AsReadOnly();
        await _cache.SetStringAsync(key, JsonSerializer.Serialize(dtos), new DistributedCacheEntryOptions()
        {
            SlidingExpiration = TimeSpan.FromMinutes(10)
        }, cancellationToken);
        return dtos;
    }
}