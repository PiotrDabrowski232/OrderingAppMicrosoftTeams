using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingApp.Data.DBConfig;
using OrderingApp.Logic.DTO;

namespace OrderingApp.Logic.Functions.Query.Order
{
    public class GetOrderBasicInfoForCurrentOrderQuery(Guid orderId) : IRequest<OrderBasicInfoDto>
    {
        public Guid OrderId { get; set; } = orderId;
    }

    public class GetOrderBasicInfoForCurrentOrderQueryHandler : BaseRequestHandler<GetOrderBasicInfoForCurrentOrderQuery, OrderBasicInfoDto>
    {

        public GetOrderBasicInfoForCurrentOrderQueryHandler(OrderingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

        }

        public override async Task<OrderBasicInfoDto> Handle(GetOrderBasicInfoForCurrentOrderQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Orders
                .Where(x => x.Id == request.OrderId)
                .Select(x => new OrderBasicInfoDto
                {
                    Id = x.Id,
                    DeliveryCost = x.DeliveryCost,
                    MinValue = x.MinValue,
                    OrderSignupCount = x.OrderSignups.Count,
                    FreeDeliveryFrom = x.FreeDeliveryFrom,

                }).FirstOrDefaultAsync(cancellationToken) ?? new OrderBasicInfoDto();
        }
    }
}
