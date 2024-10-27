namespace CommunityService.Application.DTOs.Question;

public record CreateQuestionRequest(
    int CustomerId,
    int ProductId,
    string Text);