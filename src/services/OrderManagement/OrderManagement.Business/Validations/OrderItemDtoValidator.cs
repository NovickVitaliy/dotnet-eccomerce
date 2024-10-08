using FluentValidation;
using OrderManagement.Business.DTOs.OrderItem;

namespace OrderManagement.Business.Validations;

public class OrderItemDtoValidator : BaseValidator<OrderItemDto>
{
    public OrderItemDtoValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage(IdShouldBeValid);

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage(GreaterThanGivenValue);

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage(GreaterThanGivenValue);
    }
}