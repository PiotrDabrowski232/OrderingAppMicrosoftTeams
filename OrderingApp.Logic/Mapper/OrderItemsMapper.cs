using AutoMapper;
using OrderingApp.Data.Models;
using OrderingApp.Logic.DTO;

namespace OrderingApp.Logic.Mapper
{
    public class OrderItemsMapper : Profile
    {
        public OrderItemsMapper()
        {
            CreateMap<OrderItemsDto, OrderItems>()
                .ForMember(dest => dest.DishId, opt => opt.MapFrom(src => src.Dish.Id))
                .ForMember(dest => dest.Dish, opt => opt.Ignore())
                .ForMember(dest => dest.SignupId, opt => opt.MapFrom((src, dest, destMember, context) =>
                    context.Items["SignupId"]));

            CreateMap<OrderItems, OrderItemsDto>();

            CreateMap<Dish, DishDto>();
        }
    }
}
