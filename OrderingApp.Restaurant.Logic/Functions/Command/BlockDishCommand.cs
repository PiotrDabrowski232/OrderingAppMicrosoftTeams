using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingApp.Data.DBConfig;

namespace OrderingApp.Restaurant.Logic.Functions.Command
{
    public class BlockDishCommand(string dishId) : IRequest<bool>
    {
        public string DishId { get; set; } = dishId;
    }

    public class BlockDishCommandHandler : IRequestHandler<BlockDishCommand, bool>
    {
        private readonly OrderingDbContext _dbContext;
        public BlockDishCommandHandler(OrderingDbContext dbContext)
        {
            _dbContext = dbContext;
        }   
        public async Task<bool> Handle(BlockDishCommand request, CancellationToken cancellationToken)
        {
            var dish = await _dbContext.Dishes.FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.DishId));

            dish.Blocked = true;

            _dbContext.SaveChanges();

            return true;
        }
    }
}
