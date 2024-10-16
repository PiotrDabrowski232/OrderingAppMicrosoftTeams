using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingApp.Data.DBConfig;
using OrderingApp.Logic.DTO;

namespace OrderingApp.Logic.Functions.Query.Restaurant
{
    public class GetRestaurantDishesExtrasQuery(Guid id) : IRequest<MenuDto>
    {
        public Guid Id { get; set; } = id;
    }

    public class GetRestaurantDishesExtrasQueryHandler : BaseRequestHandler<GetRestaurantDishesExtrasQuery, MenuDto>
    {
        public GetRestaurantDishesExtrasQueryHandler(OrderingDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }

        public override async Task<MenuDto> Handle(GetRestaurantDishesExtrasQuery request, CancellationToken cancellationToken)
        {
            var mainDishes = await _dbContext.Dishes
                .Where(x => x.RestaurantId == request.Id && !x.IsExtras && !x.Blocked)
                .Select(x => new DishDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Calories = x.Calories,
                    Price = x.Price,
                    IsExtras = x.IsExtras
                })
                .ToListAsync(cancellationToken);

            var extrasDishes = await _dbContext.Dishes
                .Where(x => x.RestaurantId == request.Id && x.IsExtras && !x.Blocked)
                .Select(x => new DishDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Calories = x.Calories,
                    Price = x.Price,
                    IsExtras = x.IsExtras
                })
                .ToListAsync(cancellationToken);

            return new MenuDto
            {
                Main = mainDishes,
                Extras = extrasDishes
            };
        }
    }
}
