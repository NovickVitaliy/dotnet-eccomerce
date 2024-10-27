namespace CommunityService.Application.DTOs.Question;

public record UpdateQuestionRequest(
    Guid Id,
    int CustomerId,
    int ProductId,
    string Text);