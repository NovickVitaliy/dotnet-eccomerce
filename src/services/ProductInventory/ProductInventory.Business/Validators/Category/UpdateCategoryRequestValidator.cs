using FluentValidation;
using ProductInventory.Business.DTOs.Category;

namespace ProductInventory.Business.Validators.Category;

public class UpdateCategoryRequestValidator : BaseValidator<UpdateCategoryRequest>
{
    public UpdateCategoryRequestValidator()
    {
        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage(ShouldBeGreaterThanGivenValue);

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(PropertyIsRequired);

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage(PropertyIsRequired);
    }
}