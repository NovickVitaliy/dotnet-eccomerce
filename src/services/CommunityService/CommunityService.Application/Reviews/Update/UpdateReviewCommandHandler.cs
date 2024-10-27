using Common.CQRS.Command;
using Common.ErrorHandling;
using CommunityService.Application.Data;
using CommunityService.Application.DTOs.Review;
using CommunityService.Domain.ValueObjects;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommunityService.Application.Reviews.Update;

public class UpdateReviewCommandHandler : ICommandHandler<UpdateReviewCommand>
{
    private readonly IAppDbContext _db;
    private readonly IValidator<UpdateReviewRequest> _validator;
    
    public UpdateReviewCommandHandler(IAppDbContext db, IValidator<UpdateReviewRequest> validator)
    {
        _db = db;
        _validator = validator;
    }
    
    public async Task<ErrorOr<Unit>> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.Request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return ErrorOr<Unit>.BadRequest(validationResult.Errors.FirstOrDefault()?.ErrorMessage ?? "Validation Error Occured");
        }

        var review = await _db.Reviews.FirstOrDefaultAsync(r => r.Id == ReviewId.Of(request.Request.ReviewId),
            cancellationToken);

        if (review is null)
        {
            return ErrorOr<Unit>.NotFound();
        }

        review.Comment = request.Request.Comment;
        review.Score = request.Request.Score;
        review.CustomerId = request.Request.CustomerId;
        review.ProductId = request.Request.ProductId;

        await _db.SaveChangesAsync();

        return Unit.Value;
    }
}