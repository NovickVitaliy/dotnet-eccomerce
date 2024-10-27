using Common.CQRS.Command;
using Common.ErrorHandling;
using CommunityService.Application.DTOs.Review;

namespace CommunityService.Application.Reviews.Create;

public record CreateReviewCommand(CreateReviewRequest Request) : ICommand<ErrorOr<Guid>>;