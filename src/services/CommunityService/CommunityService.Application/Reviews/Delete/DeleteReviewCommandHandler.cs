using Common.CQRS.Command;
using Common.ErrorHandling;
using CommunityService.Application.Data;
using CommunityService.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommunityService.Application.Reviews.Delete;

public class DeleteReviewCommandHandler : ICommandHandler<DeleteReviewCommand>
{
    private readonly IAppDbContext _db;
    
    public DeleteReviewCommandHandler(IAppDbContext db)
    {
        _db = db;
    }
    
    public async Task<ErrorOr<Unit>> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        var review = await _db.Reviews.FirstOrDefaultAsync(r => r.Id == ReviewId.Of(request.ReviewId), cancellationToken);
        if (review is null)
        {
            return ErrorOr<Unit>.NotFound();
        }

        _db.Reviews.Remove(review);

        await _db.SaveChangesAsync();

        return Unit.Value;
    }
}