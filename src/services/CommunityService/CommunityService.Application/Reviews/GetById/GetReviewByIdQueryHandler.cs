using AutoMapper;
using Common.CQRS.Query;
using Common.ErrorHandling;
using CommunityService.Application.Data;
using CommunityService.Application.DTOs.Review;
using CommunityService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CommunityService.Application.Reviews.GetById;

public class GetReviewByIdQueryHandler : IQueryHandler<GetReviewByIdQuery, ErrorOr<ReviewDto>>
{
    private readonly IAppDbContext _db;
    private readonly IMapper _mapper;
    
    public GetReviewByIdQueryHandler(IMapper mapper, IAppDbContext db)
    {
        _mapper = mapper;
        _db = db;
    }
    public async Task<ErrorOr<ReviewDto>> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
    {
        var review = await _db.Reviews.AsNoTracking().FirstOrDefaultAsync(r => r.Id == ReviewId.Of(request.Id), cancellationToken);
        if (review is null)
        {
            return ErrorOr<ReviewDto>.NotFound();
        }

        var reviewDto = _mapper.Map<ReviewDto>(review);

        return reviewDto;
    }
}