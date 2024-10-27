using CommunityService.Application.DTOs.Question;
using FluentValidation;

namespace CommunityService.Application.Validators;

public class CreateQuestionRequestValidator : AbstractValidator<CreateQuestionRequest>
{
    public CreateQuestionRequestValidator()
    {
        RuleFor(x => x.CustomerId)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("{PropertyName} must not be empty");
    }
}