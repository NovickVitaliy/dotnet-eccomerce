using CommunityService.Application.DTOs.Review;
using FluentValidation;

namespace CommunityService.Application.Validators;

public class CreateReviewRequestValidator : AbstractValidator<CreateReviewRequest>
{
    public CreateReviewRequestValidator()
    {
        RuleFor(x => x.Score)
            .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be bigger then 0");

        RuleFor(x => x.CustomerId)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");
    }
}