using AutoMapper;
using BasketService.Application.DTOs.Basket;
using BasketService.Domain.Domain;

namespace BasketService.Application.Profiles;

public class BasketProfile : Profile
{
    public BasketProfile()
    {
        CreateMap<CreateBasketRequest, Basket>();
        CreateMap<UpdateBasketRequest, Basket>()
            .ForMember(x => x.Id, y => y.Ignore());
        CreateMap<CreateBasketItemRequest, BasketItem>();
        CreateMap<Basket, BasketDto>();
        CreateMap<BasketItem, BasketItemDto>();
    }
}