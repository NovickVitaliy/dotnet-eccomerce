using Common.CQRS.Command;
using Common.ErrorHandling;
using CommunityService.Application.Data;
using CommunityService.Application.DTOs.Review;
using CommunityService.Domain.Models;
using CommunityService.Domain.ValueObjects;
using FluentValidation;

namespace CommunityService.Application.Reviews.Create;

public class CreateReviewCommandHandler : ICommandHandler<CreateReviewCommand, ErrorOr<Guid>>
{
    private readonly IAppDbContext _db;
    private readonly IValidator<CreateReviewRequest> _validator;

    public CreateReviewCommandHandler(IAppDbContext db, IValidator<CreateReviewRequest> validator)
    {
        _db = db;
        _validator = validator;
    }
    
    public async Task<ErrorOr<Guid>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.Request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return ErrorOr<Guid>.BadRequest(validationResult.Errors.FirstOrDefault()?.ErrorMessage ?? "Validation Error Occured");
        }

        var review = new Review()
        {
            Id = ReviewId.Of(Guid.NewGuid()),
            Comment = request.Request.Comment,
            Dislikes = 0,
            Likes = 0,
            Score = request.Request.Score,
            CreatedOn = DateTime.UtcNow,
            CustomerId = request.Request.CustomerId,
            ProductId = request.Request.ProductId,
            UpdatedOn = DateTime.UtcNow
        };

        await _db.Reviews.AddAsync(review, cancellationToken: cancellationToken);
        await _db.SaveChangesAsync();

        return review.Id.Id;
    }
}