using Common.CQRS.Command;

namespace CommunityService.Application.Questions.Delete;

public record DeleteQuestionCommand(Guid Id) : ICommand;