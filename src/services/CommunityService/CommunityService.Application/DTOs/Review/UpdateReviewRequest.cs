namespace CommunityService.Application.DTOs.Review;

public record UpdateReviewRequest(
    Guid ReviewId,
    int Score,
    int CustomerId,
    int ProductId,
    string Comment);