using AutoMapper;
using Application.DTOs;
using Domain.Entities;

namespace OrdersProject.Infrastructure.Mapping;

public class UserProfile : Profile
{
	public UserProfile()
	{
        CreateMap<User, UserResponse>()
            .ForMember(x => x.OrderIds, w => w.MapFrom(e => e.Orders.Select(o => o.Id)))
          .ReverseMap();

        CreateMap<UserAddDto, User>();
        CreateMap<UserEditDto, User>();
    }
}