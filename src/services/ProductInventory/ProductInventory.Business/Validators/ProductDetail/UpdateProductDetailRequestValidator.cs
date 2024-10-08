using FluentValidation;
using ProductInventory.Business.DTOs.ProductDetail;

namespace ProductInventory.Business.Validators.ProductDetail;

public class UpdateProductDetailRequestValidator : BaseValidator<UpdateProductDetailRequest>
{
    public UpdateProductDetailRequestValidator()
    {
        RuleFor(x => x.Weight)
            .GreaterThan(0).WithMessage(ShouldBeGreaterThanGivenValue);
        
        RuleFor(x => x.Height)
            .GreaterThan(0).WithMessage(ShouldBeGreaterThanGivenValue);
        
        RuleFor(x => x.Length)
            .GreaterThan(0).WithMessage(ShouldBeGreaterThanGivenValue);
        
        RuleFor(x => x.Width)
            .GreaterThan(0).WithMessage(ShouldBeGreaterThanGivenValue);

        RuleFor(x => x.Color)
            .NotEmpty().WithMessage(PropertyIsRequired);
    }
}