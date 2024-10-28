using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingApp.Data.DBConfig;
using OrderingApp.Restaurant.Logic.DTO;
using OrderingApp.Shared.Exceptions;

namespace OrderingApp.Restaurant.Logic.Functions.Query
{
    public class GetRestaurantQuery(string restaurantId) : IRequest<RestaurantDto>
    {
        public string RestaurantId { get; set; } = restaurantId;
    }
    public class GetRestaurantQueryHandler : IRequestHandler<GetRestaurantQuery, RestaurantDto>
    {
        private readonly OrderingDbContext _dbContext;
        public GetRestaurantQueryHandler(OrderingDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<RestaurantDto> Handle(GetRestaurantQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Restaurants.Where(x => x.Id == Guid.Parse(request.RestaurantId)).Select(x => new RestaurantDto
            {
                Id = x.Id,
                Name = x.Name,
                RestaurantType = Enum.GetName(x.RestaurantType)
            }).FirstOrDefaultAsync(cancellationToken) ?? throw new EntityNotFoundException("There are not restaurant with provided id");
        }
    }
}
