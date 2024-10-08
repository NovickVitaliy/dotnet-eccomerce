using FluentValidation;
using ProductInventory.Business.DTOs.Supplier;

namespace ProductInventory.Business.Validators.Supplier;

public class UpdateSupplierRequestValidator : BaseValidator<UpdateSupplierRequest>
{
    public UpdateSupplierRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(PropertyIsRequired);

        RuleFor(x => x.ContactInfo)
            .NotEmpty().WithMessage(PropertyIsRequired);

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage(PropertyIsRequired);
    }
}