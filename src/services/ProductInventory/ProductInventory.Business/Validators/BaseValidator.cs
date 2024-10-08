using FluentValidation;

namespace ProductInventory.Business.Validators;

public abstract class BaseValidator<TEntity> : AbstractValidator<TEntity>
{
    protected const string PropertyIsRequired = "{PropertyName} is required";
    protected const string ShouldBeGreaterThanGivenValue = "{PropertyName} should be greater than {ComparisonValue}";

    protected BaseValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
    }
}