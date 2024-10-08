using FluentValidation;
using OrderManagement.Business.DTOs.Order;

namespace OrderManagement.Business.Validations;

public class CreateOrderRequestValidator : BaseValidator<CreateOrderRequest>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage(PropertyRequired)
            .GreaterThan(0).WithMessage(GreaterThanGivenValue);

        RuleFor(x => x.ShippingAddres)
            .NotEmpty().WithMessage(PropertyRequired);

        RuleFor(x => x.OrderItems)
            .NotNull().WithMessage(PropertyRequired);

        RuleFor(x => x.OrderItems)
            .ForEach(item => item.SetValidator(new OrderItemDtoValidator()))
            .WithMessage("At least one item is required.");
    }
}