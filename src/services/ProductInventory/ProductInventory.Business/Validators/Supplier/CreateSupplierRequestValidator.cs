using FluentValidation;
using ProductInventory.Business.DTOs.Supplier;

namespace ProductInventory.Business.Validators.Supplier;

public class CreateSupplierRequestValidator : BaseValidator<CreateSupplierRequest>
{
    public CreateSupplierRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(PropertyIsRequired);
        
        RuleFor(x => x.ContactInfo)
            .NotEmpty().WithMessage(PropertyIsRequired);
        
        RuleFor(x => x.Address)
            .NotEmpty().WithMessage(PropertyIsRequired);
    }
}