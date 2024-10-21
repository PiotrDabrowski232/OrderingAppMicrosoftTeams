using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using OrderingApp.Data.DBConfig;

namespace OrderingApp.Logic.BackgroundJobs
{
    public class BlockedDishesInformer : BackgroundService
    {
        private readonly OrderingDbContext _dbContext;
        private Timer? _timer = null;

        public BlockedDishesInformer(OrderingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }

        private void DoWork()
        {
            var orders =  _dbContext.Dishes.Where(x => x.Blocked)
                .Include(x => x.OrderItems)
                    .ThenInclude(x => x.Select(x => x.Signup))
                        .ThenInclude(x => x.Order)
                            .ToList();

        }
    }
}
