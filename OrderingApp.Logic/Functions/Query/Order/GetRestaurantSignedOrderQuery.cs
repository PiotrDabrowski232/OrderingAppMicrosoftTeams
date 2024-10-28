using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingApp.Data.DBConfig;

namespace OrderingApp.Logic.Functions.Query.Order
{
    public class GetRestaurantSignedOrderQuery(Guid Id) : IRequest<Guid> 
    {
        public Guid Id { get; set; } = Id;
    }

    public class GetRestaurantSignedOrderQueryHandler : BaseRequestHandler<GetRestaurantSignedOrderQuery, Guid>
    {
        public GetRestaurantSignedOrderQueryHandler(OrderingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<Guid> Handle(GetRestaurantSignedOrderQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Orders
                .Where(x => x.Id == request.Id)
                .Select(x => x.RestaurantId)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
