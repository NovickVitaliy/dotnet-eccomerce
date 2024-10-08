using FluentValidation;
using ProductInventory.Business.DTOs.ProductTag;

namespace ProductInventory.Business.Validators.ProductTag;

public class CreateProductTagRequestValidator : BaseValidator<CreateProductTagRequest>
{
    public CreateProductTagRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(PropertyIsRequired);
    }
}