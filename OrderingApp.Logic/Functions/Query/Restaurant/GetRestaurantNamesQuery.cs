using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingApp.Data.DBConfig;

namespace OrderingApp.Logic.Functions.Query.Restaurant
{
    public class GetRestaurantNamesQuery : IRequest<IDictionary<Guid, string>>;
    public class GetRestaurantNamesQueryHandler : BaseRequestHandler<GetRestaurantNamesQuery, IDictionary<Guid, string>>
    {
        public GetRestaurantNamesQueryHandler(OrderingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<IDictionary<Guid, string>> Handle(GetRestaurantNamesQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Restaurants
                .Select(x => new { x.Id, x.Name })
                .OrderBy(x => x.Name)
                .ToDictionaryAsync(x => x.Id, x => x.Name, cancellationToken);

            return result;
        }
    }
}
