namespace BasketService.Application.DTOs.Basket;

public record BasketItemDto(string Id, int Quantity, int PricePerUnit, int ProductId, string ProductName);