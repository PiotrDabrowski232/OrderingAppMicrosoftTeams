using MediatR;
using OrderingApp.Data.DBConfig;
using OrderingApp.Restaurant.Logic.DTO;
using Microsoft.EntityFrameworkCore;

namespace OrderingApp.Restaurant.Logic.Functions.Query
{
    public class GetAllRestaurantQuery : IRequest<IEnumerable<RestaurantDto>>;

    public class GetAllRestaurantQueryHandler : IRequestHandler<GetAllRestaurantQuery, IEnumerable<RestaurantDto>>
    {
        private readonly OrderingDbContext _dbContext;
        public GetAllRestaurantQueryHandler(OrderingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<RestaurantDto>> Handle(GetAllRestaurantQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Restaurants
                .Select(x => new RestaurantDto
                {
                    Name = x.Name,
                    Id = x.Id,
                    RestaurantType = Enum.GetName(x.RestaurantType)
                }).ToListAsync();
        }
    }
}
