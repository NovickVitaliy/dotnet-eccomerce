using FluentValidation;
using ProductInventory.Business.DTOs.Product;

namespace ProductInventory.Business.Validators.Product;

public class CreateProductRequestValidator : BaseValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(PropertyIsRequired);
        
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage(PropertyIsRequired);

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage(ShouldBeGreaterThanGivenValue);
        
        RuleFor(x => x.StockQuantity)
            .GreaterThan(0).WithMessage(ShouldBeGreaterThanGivenValue);
        
        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage(ShouldBeGreaterThanGivenValue);
        
        RuleFor(x => x.SupplierId)
            .GreaterThan(0).WithMessage(ShouldBeGreaterThanGivenValue);
    }
}