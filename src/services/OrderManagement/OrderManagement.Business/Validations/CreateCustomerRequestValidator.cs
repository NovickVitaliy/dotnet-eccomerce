using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using FluentValidation.Validators;
using OrderManagement.Business.DTOs.Customer;
using OrderManagement.Business.Validations.Constants;
using OrderManagement.Domain.Models;

namespace OrderManagement.Business.Validations;

public class CreateCustomerRequestValidator : BaseValidator<CreateCustomerRequest>
{
    public CreateCustomerRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage(PropertyRequired);

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage(PropertyRequired);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(PropertyRequired)
            .EmailAddress().WithMessage(EmailShouldBeInValidFormat);

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage(PropertyRequired)
            .Matches(Expressions.UkrainianPhoneNumber).WithMessage(PhoneNumberShouldBeInValidUkrainianFormat);
    }
}