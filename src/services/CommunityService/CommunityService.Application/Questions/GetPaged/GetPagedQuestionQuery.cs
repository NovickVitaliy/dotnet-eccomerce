using Common.CQRS.Query;
using Common.ErrorHandling;
using CommunityService.Application.DTOs.Question;

namespace CommunityService.Application.Questions.GetPaged;

public record GetPagedQuestionQuery(int PageNumber, int PageSize) : IQuery<ErrorOr<IReadOnlyCollection<QuestionDto>>>;