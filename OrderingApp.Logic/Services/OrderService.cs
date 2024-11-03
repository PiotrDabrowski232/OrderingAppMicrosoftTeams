using Microsoft.EntityFrameworkCore;
using OrderingApp.Data.DBConfig;
using OrderingApp.Data.Models;
using OrderingApp.Logic.Services.Interface;

namespace OrderingApp.Logic.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderingDbContext _dbContext;
        public OrderService(OrderingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> OrderNameExist(string name, Guid? Id, CancellationToken cancellationToken)
        {
            return await _dbContext.Orders
                .Where(x => x.Status == Data.Models.Enum.OrderStatus.Active && x.Name == name && (!Id.HasValue || x.Id != Id.Value))
                .AnyAsync(cancellationToken);
        }

        public async Task<Order> GetBasicOrderInfo(Guid Id, CancellationToken cancellationToken)
        {
            return await _dbContext.Orders
                .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        }
    }
}
