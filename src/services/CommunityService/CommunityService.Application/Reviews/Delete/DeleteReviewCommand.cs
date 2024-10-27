using Common.CQRS.Command;

namespace CommunityService.Application.Reviews.Delete;

public record DeleteReviewCommand(Guid ReviewId) : ICommand;