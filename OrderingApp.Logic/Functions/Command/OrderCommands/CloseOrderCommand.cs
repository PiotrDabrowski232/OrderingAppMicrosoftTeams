using AutoMapper;
using MediatR;
using OrderingApp.Data.DBConfig;
using OrderingApp.Logic.Services.Interface;

namespace OrderingApp.Logic.Functions.Command.OrderCommands
{
    public class CloseOrderCommand(Guid id) : IRequest<bool>
    {
        public Guid Id { get; set; } = id;
    }

    public class CloseOrderCommandHandler : BaseRequestHandler<CloseOrderCommand, bool>
    {
        private readonly IOrderService _orderService;
        public CloseOrderCommandHandler(OrderingDbContext dbContext, IMapper mapper, IOrderService orderService) : base(dbContext, mapper)
        {
            _orderService = orderService;
        }

        public override async Task<bool> Handle(CloseOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var order = await _orderService.GetBasicOrderInfo(request.Id, cancellationToken);

                order.IsActive = false;

                _dbContext.SaveChanges();

                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
