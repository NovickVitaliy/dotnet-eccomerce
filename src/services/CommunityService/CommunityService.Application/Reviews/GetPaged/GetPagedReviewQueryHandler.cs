using AutoMapper;
using Common.CQRS.Query;
using Common.ErrorHandling;
using CommunityService.Application.Data;
using CommunityService.Application.DTOs.Review;
using Microsoft.EntityFrameworkCore;

namespace CommunityService.Application.Reviews.GetPaged;

public class GetPagedReviewQueryHandler : IQueryHandler<GetPagedReviewQuery, ErrorOr<IReadOnlyCollection<ReviewDto>>>
{
    private readonly IAppDbContext _db;
    private readonly IMapper _mapper;
    
    public GetPagedReviewQueryHandler(IAppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    
    public async Task<ErrorOr<IReadOnlyCollection<ReviewDto>>> Handle(GetPagedReviewQuery request, CancellationToken cancellationToken)
    {
        var skip = (request.PageNumber - 1) * request.PageSize;
        var take = request.PageSize;

        var reviews = await _db.Reviews.AsNoTracking()
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken: cancellationToken);

        return reviews.Select(r => _mapper.Map<ReviewDto>(r)).ToList();
    }
}