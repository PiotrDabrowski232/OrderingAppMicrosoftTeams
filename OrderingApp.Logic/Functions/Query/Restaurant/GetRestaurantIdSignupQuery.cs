using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingApp.Data.DBConfig;

namespace OrderingApp.Logic.Functions.Query.Restaurant
{
    public class GetRestaurantIdSignupQuery(Guid signupId) : IRequest<Guid>
    {
        public Guid SignupId { get; set; } = signupId;
    }

    public class GetRestaurantIdSignupQueryHandler : BaseRequestHandler<GetRestaurantIdSignupQuery, Guid>
    {
        public GetRestaurantIdSignupQueryHandler(OrderingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<Guid> Handle(GetRestaurantIdSignupQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.OrderSignups
                .Where(x => x.Id == request.SignupId)
                .Select(x => x.Order.RestaurantId)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
