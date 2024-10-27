using Common.CQRS.Command;
using CommunityService.Application.DTOs.Question;

namespace CommunityService.Application.Questions.Update;

public record UpdateQuestionCommand(UpdateQuestionRequest Request) : ICommand;