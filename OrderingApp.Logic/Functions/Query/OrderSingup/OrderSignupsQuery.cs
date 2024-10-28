using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingApp.Data.DBConfig;

namespace OrderingApp.Logic.Functions.Query.OrderSingup
{
    public class OrderSignupsQuery(Guid id) : IRequest<bool>
    {
        public Guid Id { get; set; } = id;
    }
    public class OrderSignupsQueryHandler : BaseRequestHandler<OrderSignupsQuery, bool>
    {
        public OrderSignupsQueryHandler(OrderingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<bool> Handle(OrderSignupsQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Orders
                .Where(order => order.Id == request.Id)
                .Include(order => order.OrderSignups)
                .FirstOrDefaultAsync(cancellationToken);

            return result.OrderSignups.Any();
        }
    }
}
