using FluentValidation;
using ProductInventory.Business.DTOs.ProductTag;

namespace ProductInventory.Business.Validators.ProductTag;

public class UpdateProductTagRequestValidator :  BaseValidator<UpdateProductTagRequest>
{
    public UpdateProductTagRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(PropertyIsRequired);
    }
}