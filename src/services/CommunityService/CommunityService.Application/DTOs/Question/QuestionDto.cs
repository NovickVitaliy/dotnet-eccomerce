namespace CommunityService.Application.DTOs.Question;

public record QuestionDto(
    Guid Id,
    int CustomerId,
    int ProductId,
    string Text);