using Common.CQRS.Query;
using Common.ErrorHandling;
using CommunityService.Application.DTOs.Question;

namespace CommunityService.Application.Questions.GetById;

public record GetQuestionByIdQuery(Guid Id) : IQuery<ErrorOr<QuestionDto>>;