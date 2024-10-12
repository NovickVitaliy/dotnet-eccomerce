using BasketService.Application.DTOs.Basket;
using Common.ErrorHandling;

namespace BasketService.Application.Contracts;

public interface IBasketService
{
    Task<ErrorOr<string>> CreateBasketAsync(CreateBasketRequest request);
    Task<ErrorOr<bool>> UpdateBasketAsync(UpdateBasketRequest request);
    Task<ErrorOr<bool>> DeleteBasketAsync(string basketId);
    Task<ErrorOr<BasketDto>> GetBasketByUserIdAsync(int userId);
}