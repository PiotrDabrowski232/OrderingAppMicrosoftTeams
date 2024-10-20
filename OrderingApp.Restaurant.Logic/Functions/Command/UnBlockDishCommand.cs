using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingApp.Data.DBConfig;

namespace OrderingApp.Restaurant.Logic.Functions.Command
{
    public class UnBlockDishCommand(string dishId) : IRequest<bool>
    {
        public string DishId { get; set; } = dishId;
    }

    public class UnBlockDishCommandHandler : IRequestHandler<UnBlockDishCommand, bool>
    {
        private readonly OrderingDbContext _dbContext;
        public UnBlockDishCommandHandler(OrderingDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Handle(UnBlockDishCommand request, CancellationToken cancellationToken)
        {
            var dish = await _dbContext.Dishes.FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.DishId));

            dish.Blocked = false;

            _dbContext.SaveChanges();

            return true;
        }
    }
}
