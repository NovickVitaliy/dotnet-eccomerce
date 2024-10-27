namespace CommunityService.Application.DTOs.Review;

public record CreateReviewRequest(
    int Score,
    int CustomerId,
    int ProductId,
    string Comment);