using MediatR;
using OrderingApp.Data.DBConfig;
using OrderingApp.Restaurant.Logic.DTO;

namespace OrderingApp.Restaurant.Logic.Functions.Command
{
    public class AddDishCommand(string restaurantId, NewDishDto dish) : IRequest<Guid>
    {
        public string RestaurantId { get; set; } = restaurantId;
        public NewDishDto Dish { get; set; } = dish;
    }

    public class AddDishCommandHandler : IRequestHandler<AddDishCommand, Guid>
    {
        private readonly OrderingDbContext _dbContext;
        public AddDishCommandHandler(OrderingDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Guid> Handle(AddDishCommand request, CancellationToken cancellationToken)
        {
            var dish = new Data.Models.Dish
            {
                Id = Guid.NewGuid(),
                Name = request.Dish.Name,
                Description = request.Dish.Description,
                Price = request.Dish.Price,
                Calories = request.Dish.Calories,
                IsExtras = request.Dish.IsExtras,
                Blocked = false,
                RestaurantId = Guid.Parse(request.RestaurantId)
            };

            _dbContext.Dishes.Add(dish);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return dish.Id;
        }
    }
}
