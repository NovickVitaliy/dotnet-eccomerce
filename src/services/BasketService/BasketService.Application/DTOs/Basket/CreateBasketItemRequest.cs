using System.ComponentModel.DataAnnotations;

namespace BasketService.Application.DTOs.Basket;

public class CreateBasketItemRequest
{
    [Required]
    [Range(minimum:1, maximum:int.MaxValue)]
    public int Quantity { get; init; }
    
    [Required]
    [Range(minimum:1, maximum:int.MaxValue)]
    public int PricePerUnit { get; init; }
    
    [Required]
    [Range(minimum:1, maximum:int.MaxValue)]
    public int ProductId { get; init; }
    
    [Required]
    public string ProductName { get; init; }
}