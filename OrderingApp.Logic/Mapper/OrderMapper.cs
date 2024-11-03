using AutoMapper;
using OrderingApp.Data.Models;
using OrderingApp.Data.Models.Enum;
using OrderingApp.Logic.DTO;

namespace OrderingApp.Logic.Mapper
{
    public class OrderMapper : Profile
    {
        public OrderMapper()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.RestaurantId, opt => opt.MapFrom(src => src.RestaurantId.ToString()))
                .ForMember(dest => dest.BankAccountNumber, opt => opt.MapFrom(src => src.BankAccountNumber.ToString()));

            CreateMap<OrderDto, Order>()
                .ForMember(dest => dest.RestaurantId, opt => opt.MapFrom(src => Guid.Parse(src.RestaurantId)))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => OrderStatus.Active))
                .ForMember(dest => dest.BankAccountNumber, opt => opt.MapFrom(src => Decimal.Parse(src.BankAccountNumber)));


            CreateMap<Order, OrderDetailsDto>()
                .ForMember(dest => dest.RestaurantName, opt => opt.MapFrom(src => src.Restaurant.Name))
                .ForMember(dest => dest.RestaurantType, opt => opt.MapFrom(src => Enum.GetName(typeof(RestauranType), src.Restaurant.RestaurantType)));

            CreateMap<Order, OrderInformationDto>()
                .ForMember(dest => dest.Author, opt => opt.Ignore())
                .ForMember(dest => dest.Myorder, opt => opt.MapFrom((src, dest, destMember, context) =>
                    (Guid)context.Items["UserId"] == src.CreatedBy))
                .ForMember(dest => dest.Signups, opt => opt.MapFrom(src => src.OrderSignups))
                .ForMember(dest => dest.IsSignedUp, opt => opt.MapFrom((src, dest, destMember, context) =>
                    src.OrderSignups.Any(signup => signup.SignedUser == (Guid)context.Items["UserId"])))
                .ForMember(dest => dest.RestaurantName, opt => opt.MapFrom(src => src.Restaurant.Name))
                .ForMember(dest => dest.RestaurantType, opt => opt.MapFrom(src => Enum.GetName(typeof(RestauranType), src.Restaurant.RestaurantType)));

            CreateMap<OrderSignups, OrderSignupsDto>();
        }
    }
}