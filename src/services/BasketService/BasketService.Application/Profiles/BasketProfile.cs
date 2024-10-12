using AutoMapper;
using BasketService.Application.DTOs.Basket;
using BasketService.Domain.Domain;

namespace BasketService.Application.Profiles;

public class BasketProfile : Profile
{
    public BasketProfile()
    {
        CreateMap<CreateBasketRequest, Basket>();
        CreateMap<CreateBasketItemRequest, CreateBasketItemRequest>();
        CreateMap<Basket, BasketDto>();
        CreateMap<BasketItem, BasketItemDto>();
    }
}