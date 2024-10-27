using Common.CQRS.Query;
using Common.ErrorHandling;
using CommunityService.Application.DTOs.Review;

namespace CommunityService.Application.Reviews.GetById;

public record GetReviewByIdQuery(Guid Id) : IQuery<ErrorOr<ReviewDto>>;