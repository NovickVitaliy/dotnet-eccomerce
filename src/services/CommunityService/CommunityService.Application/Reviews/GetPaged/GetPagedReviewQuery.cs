using Common.CQRS.Query;
using Common.ErrorHandling;
using CommunityService.Application.DTOs.Review;

namespace CommunityService.Application.Reviews.GetPaged;

public record GetPagedReviewQuery(int PageNumber, int PageSize) : IQuery<ErrorOr<IReadOnlyCollection<ReviewDto>>>;