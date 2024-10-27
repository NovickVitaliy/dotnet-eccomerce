namespace CommunityService.Application.DTOs.Review;

public record ReviewDto(
        Guid Id,
        int Score,
        int CustomerId,
        int ProductId,
        string Comment,
        int Likes,
        int Dislikes);