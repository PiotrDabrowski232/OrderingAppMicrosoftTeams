using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingApp.Data.DBConfig;
using OrderingApp.Logic.DTO;

namespace OrderingApp.Logic.Functions.Query.Order
{
    public class GetOrderBasicConfigurationQuery(Guid signupId) : IRequest<OrderBasicInfoDto>
    {
        public Guid SignupId { get; set; } = signupId;
    }

    public class GetOrderBasicConfigurationQueryHandler : BaseRequestHandler<GetOrderBasicConfigurationQuery, OrderBasicInfoDto>
    {

        public GetOrderBasicConfigurationQueryHandler(OrderingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

        }

        public override async Task<OrderBasicInfoDto> Handle(GetOrderBasicConfigurationQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.OrderSignups
                .Where(x => x.Id == request.SignupId)
                .Select(x => new OrderBasicInfoDto
                {
                    Id = x.OrderId,
                    DeliveryCost = x.Order.DeliveryCost,
                    MinValue = x.Order.MinValue,
                    OrderSignupCount = x.Order.OrderSignups.Count,
                    FreeDeliveryFrom = x.Order.FreeDeliveryFrom,
                    Status = x.Order.Status

                }).FirstOrDefaultAsync(cancellationToken) ?? new OrderBasicInfoDto();
        }
    }
}
