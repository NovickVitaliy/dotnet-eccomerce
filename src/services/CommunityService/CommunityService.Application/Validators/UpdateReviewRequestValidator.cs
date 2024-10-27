using CommunityService.Application.DTOs.Review;
using FluentValidation;

namespace CommunityService.Application.Validators;

public class UpdateReviewRequestValidator : AbstractValidator<UpdateReviewRequest>
{
    public UpdateReviewRequestValidator()
    {
        RuleFor(x => x.ReviewId)
            .NotEmpty().WithMessage("{PropertyName} must not be empty");

        RuleFor(x => x.Score)
            .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be greater than or equal to 0");

        RuleFor(x => x.CustomerId)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");
    }
}