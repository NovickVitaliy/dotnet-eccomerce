using FluentValidation;

namespace OrderManagement.Business.Validations;

public abstract class BaseValidator<TEntity> : AbstractValidator<TEntity>
{
    protected const string IdShouldBeValid = "{PropertyName} should be valid";
    protected const string PropertyRequired = "{PropertyName} is required.";
    protected const string EmailShouldBeInValidFormat = "{PropertyName} should be in valid email format.";
    protected const string PhoneNumberShouldBeInValidUkrainianFormat = "{PropertyName} should be in valid ukrainian format.";
    protected const string GreaterThanGivenValue = "{PropertName} should be greater than {ComparisonValue}";
    
    protected BaseValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;
    }
}