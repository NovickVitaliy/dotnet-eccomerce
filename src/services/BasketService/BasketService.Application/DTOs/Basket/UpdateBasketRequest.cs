using System.ComponentModel.DataAnnotations;

namespace BasketService.Application.DTOs.Basket;

public class UpdateBasketRequest
{
    [Required]
    [Range(minimum: 1, maximum: int.MaxValue)]
    public int UserId { get; init; }

    [Required]
    [Range(minimum: 1, maximum: int.MaxValue)]
    public decimal Price { get; init; }

    [Required] [MinLength(1)] 
    public IReadOnlyCollection<CreateBasketItemRequest> BasketItems { get; init; }
}