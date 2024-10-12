namespace BasketService.Application.DTOs.Basket;

public record BasketDto(string Id, int UserId, IReadOnlyCollection<BasketItemDto> BasketItems, decimal Price);