using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingApp.Data.DBConfig;
using OrderingApp.Logic.DTO;
using OrderingApp.Logic.Services.Interface;

namespace OrderingApp.Logic.Functions.Query.Order
{
    public class GetOrderForUpdateQuery : IRequest<OrderDto>
    {
        public Guid OrderId { get; set; }
        public GetOrderForUpdateQuery(Guid id) => OrderId = id;
    }
    public class GetOrderForUpdateQueryHandler : BaseRequestHandler<GetOrderForUpdateQuery, OrderDto>
    {
        private readonly IOrderService _orderService;
        public GetOrderForUpdateQueryHandler(OrderingDbContext dbContext, IMapper mapper, IOrderService orderService) : base(dbContext, mapper)
        {
            _orderService = orderService;
        }

        public override async Task<OrderDto> Handle(GetOrderForUpdateQuery request, CancellationToken cancellationToken)
        {
            var result = await _orderService.GetBasicOrderInfo(request.OrderId, cancellationToken);

            return _mapper.Map<OrderDto>(result);
        }
    }
}
