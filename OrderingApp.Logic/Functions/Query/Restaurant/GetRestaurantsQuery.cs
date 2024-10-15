using AutoMapper;
using MediatR;
using OrderingApp.Data.DBConfig;
using OrderingApp.Data.Models.Enum;
using OrderingApp.Logic.DTO;
using Microsoft.EntityFrameworkCore;

namespace OrderingApp.Logic.Functions.Query.Restaurant
{
    public class GetRestaurantsQuery : IRequest<(IEnumerable<RestaurantDto>, IList<string>)>;

    public class GetRestaurantsQueryHandler : BaseRequestHandler<GetRestaurantsQuery, (IEnumerable<RestaurantDto>, IList<string>)>
    {
        public GetRestaurantsQueryHandler(OrderingDbContext _dbContext, IMapper _mapper) : base(_dbContext, _mapper) { }

        public override async Task<(IEnumerable<RestaurantDto>, IList<string>)> Handle(GetRestaurantsQuery request, CancellationToken cancellationToken)
        {
            var restaurants = await _dbContext.Restaurants.Select(x => new RestaurantDto
            {
                Id = x.Id,
                Name = x.Name,
                Type = Enum.GetName(x.RestaurantType)
            }).ToListAsync();

            var types = Enum.GetNames(typeof(RestauranType)).ToList();

            return (restaurants,types);
        }
    }
}
