using FluentValidation;
using ProductInventory.Business.DTOs.Category;

namespace ProductInventory.Business.Validators.Category;

public class CreateCategoryRequestValidator : BaseValidator<CreateCategoryRequest>
{
    public CreateCategoryRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(PropertyIsRequired);

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage(PropertyIsRequired);
    }
}