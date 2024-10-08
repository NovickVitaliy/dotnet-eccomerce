using AutoMapper;
using OrderManagement.Business.DTOs.Order;
using OrderManagement.Business.DTOs.OrderHistory;
using OrderManagement.Business.DTOs.OrderItem;
using OrderManagement.Domain.Models;

namespace OrderManagement.Business.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<CreateOrderRequest, Order>();
        CreateMap<OrderItemDto, OrderItem>();
        CreateMap<OrderItem, OrderItemDto>();
        CreateMap<Order, OrderDto>();
        CreateMap<OrderHistory, OrderHistoryDto>();
    }
}