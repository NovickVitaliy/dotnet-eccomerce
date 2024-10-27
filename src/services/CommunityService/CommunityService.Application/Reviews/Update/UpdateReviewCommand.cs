using Common.CQRS.Command;
using CommunityService.Application.DTOs.Review;

namespace CommunityService.Application.Reviews.Update;

public record UpdateReviewCommand(UpdateReviewRequest Request) : ICommand;