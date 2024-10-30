using System.Text.Json;
using AutoMapper;
using Common.CQRS.Query;
using Common.ErrorHandling;
using CommunityService.Application.Data;
using CommunityService.Application.DTOs.Review;
using CommunityService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace CommunityService.Application.Reviews.GetById;

public class GetReviewByIdQueryHandler : IQueryHandler<GetReviewByIdQuery, ErrorOr<ReviewDto>>
{
    private readonly IAppDbContext _db;
    private readonly IMapper _mapper;
    private readonly IDistributedCache _cache;

    public GetReviewByIdQueryHandler(IMapper mapper, IAppDbContext db, IDistributedCache cache)
    {
        _mapper = mapper;
        _db = db;
        _cache = cache;
    }
    public async Task<ErrorOr<ReviewDto>> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
    {
        var key = $"review-{request.Id}";
        var cached = await _cache.GetStringAsync(key, cancellationToken);
        if (!string.IsNullOrEmpty(cached))
        {
            return JsonSerializer.Deserialize<ReviewDto>(cached)!;
        }

        var review = await _db.Reviews.AsNoTracking().FirstOrDefaultAsync(r => r.Id == ReviewId.Of(request.Id), cancellationToken);
        if (review is null)
        {
            return ErrorOr<ReviewDto>.NotFound();
        }

        var reviewDto = _mapper.Map<ReviewDto>(review);
        await _cache.SetStringAsync(key, JsonSerializer.Serialize(reviewDto), new DistributedCacheEntryOptions()
        {
            SlidingExpiration = TimeSpan.FromMinutes(10)
        }, cancellationToken);
        return reviewDto;
    }
}