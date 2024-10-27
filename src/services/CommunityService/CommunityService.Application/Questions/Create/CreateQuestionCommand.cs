using Common.CQRS.Command;
using Common.ErrorHandling;
using CommunityService.Application.DTOs.Question;

namespace CommunityService.Application.Questions.Create;

public record CreateQuestionCommand(CreateQuestionRequest Request) : ICommand<ErrorOr<Guid>>;