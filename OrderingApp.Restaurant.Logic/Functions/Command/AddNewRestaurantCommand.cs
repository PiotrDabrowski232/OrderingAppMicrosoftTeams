using MediatR;
using OrderingApp.Data.DBConfig;
using OrderingApp.Data.Models.Enum;
using OrderingApp.Restaurant.Logic.DTO;

namespace OrderingApp.Restaurant.Logic.Functions.Command
{
    public class AddNewRestaurantCommand(RestaurantDto restaurant)  : IRequest<Guid>
    {
        public RestaurantDto Restaurant { get; set; } = restaurant;
    }

    public class AddNewRestaurantCommandHandler : IRequestHandler<AddNewRestaurantCommand, Guid>
    {
        private readonly OrderingDbContext _dbContext;
        public AddNewRestaurantCommandHandler(OrderingDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Guid> Handle(AddNewRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurant = new Data.Models.Restaurant
            {
                Id = Guid.NewGuid(),
                Name = request.Restaurant.Name,
                RestaurantType = (RestauranType)Enum.Parse(typeof(RestauranType), request.Restaurant.RestaurantType)
            };

            _dbContext.Restaurants.Add(restaurant);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return restaurant.Id;
        }
    }
}
