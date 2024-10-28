using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingApp.Data.DBConfig;
using OrderingApp.Restaurant.Logic.DTO;

namespace OrderingApp.Restaurant.Logic.Functions.Query
{
    public class GetAllRestaurantDishesQuery(string restaurantId) : IRequest<List<DishDto>>
    {
        public string RestaurantId { get; set; } = restaurantId;
    }

    public class GetAllRestaurantDishesQueryHandler : IRequestHandler<GetAllRestaurantDishesQuery, List<DishDto>>
    {
        private readonly OrderingDbContext _dbContext;
        public GetAllRestaurantDishesQueryHandler(OrderingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<DishDto>> Handle(GetAllRestaurantDishesQuery request, CancellationToken cancellationToken)
        {
            return _dbContext.Dishes
                .Where(x => x.RestaurantId == Guid.Parse(request.RestaurantId))
                .Select(x => new DishDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Calories = x.Calories,
                    IsExtras = x.IsExtras,
                    Blocked = x.Blocked,
                }).ToListAsync(cancellationToken);
        }
    }
}
