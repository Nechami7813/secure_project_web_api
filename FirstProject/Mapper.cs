using AutoMapper;
using Entities;
using DTOs;
using Entities.product;
using Microsoft.AspNetCore.Mvc;

namespace FirstProject;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<OrderItemDto, OrderItem>();
        CreateMap<UserDTO, User>();
        CreateMap<LoginUserDTO, User>();
        CreateMap<User,ReturnUserDto>();

        CreateMap<CreateOrderDTO, Order>()
            .ForMember(dest => dest.OrderItems,
                opts => opts.MapFrom(src => src.OrderItemDTOs))
            .ForMember(dest => dest.OrderDate,
                opts => opts.MapFrom(src => DateOnly.FromDateTime(DateTime.UtcNow)));

        CreateMap<Order, CreateOrderDTO>();

        CreateMap<Product, ProductDto>().ForMember(dest => dest.CategoryName,
            opts => opts.MapFrom(src => src.Category.CategoryName));

        CreateMap<Order, ReturnOrderDTO>();
        //.ForMember(dest => dest.OrderItemDTOs,
        //    opts => opts.MapFrom(src => src.OrderItems))
        //.ForMember(dest => dest.OrderDate,
        //    opts => opts.MapFrom(src => src.OrderDate));
    }
}