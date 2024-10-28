using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingApp.Data.DBConfig;
using OrderingApp.Logic.DTO;

namespace OrderingApp.Logic.Functions.Query.OrderSingup
{
    public class GetSignupDetailsQuery(Guid signupId) : IRequest<(OrderSignupsDto, List<OrderItemsDto>)>
    {
        public Guid SignupId { get; set; } = signupId;
    }

    public class GetSignupDetailsQueryHandler : BaseRequestHandler<GetSignupDetailsQuery, (OrderSignupsDto, List<OrderItemsDto>)>
    {
        public GetSignupDetailsQueryHandler(OrderingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<(OrderSignupsDto, List<OrderItemsDto>)> Handle(GetSignupDetailsQuery request, CancellationToken cancellationToken)
        {
            var signupDetails = await _dbContext.OrderItems
                .Where(x => x.SignupId == request.SignupId)
                .Include(x => x.Signup)
                .Include(x => x.Dish)
                .ToListAsync(cancellationToken);


            var mappedSignup = _mapper.Map<OrderSignupsDto>(signupDetails.Select(x => x.Signup).First());

            var mappedOrderItems = _mapper.Map<List<OrderItemsDto>>(signupDetails);

            return (mappedSignup, mappedOrderItems);
        }
    }
}
