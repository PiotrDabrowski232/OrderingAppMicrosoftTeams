using AutoMapper;
using OrderingApp.Data.Models;
using OrderingApp.Logic.DTO;

namespace OrderingApp.Logic.Mapper
{
    public class SignupMapper : Profile
    {
        public SignupMapper()
        {
            CreateMap<OrderSignups, OrderSignupsDto>();
            CreateMap<OrderSignupsDto, OrderSignups>();
        }
    }
}
