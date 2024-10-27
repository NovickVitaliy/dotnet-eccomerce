using Common.CQRS.Command;
using Common.ErrorHandling;
using CommunityService.Application.Data;
using CommunityService.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommunityService.Application.Questions.Delete;

public class DeleteQuestionCommandHandler : ICommandHandler<DeleteQuestionCommand>
{
    private readonly IAppDbContext _db;
    
    public DeleteQuestionCommandHandler(IAppDbContext db)
    {
        _db = db;
    }
    
    public async Task<ErrorOr<Unit>> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
    {
        var question = await _db.Questions.FirstOrDefaultAsync(q => q.Id == QuestionId.Of(request.Id),
            cancellationToken: cancellationToken);

        if (question is null)
        {
            return ErrorOr<Unit>.NotFound();
        }

        _db.Questions.Remove(question);
        await _db.SaveChangesAsync();

        return Unit.Value;
    }
}