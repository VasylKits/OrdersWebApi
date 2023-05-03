using AutoMapper;
using Application.DTOs;
using Domain.Entities;

namespace Infrastructure.Mapping;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderResponse>()
            .ForMember(o => o.UserLogin, w => w.MapFrom(u => u.User.Login))
            .ReverseMap();

        CreateMap<OrderAddDto, Order>();
        CreateMap<OrderEditDto, Order>();
    }
}