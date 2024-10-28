using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingApp.Data.DBConfig;
using OrderingApp.Logic.Services.Interface;
using OrderingApp.Shared.Exceptions;

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
            var anyDishBlocked = await _dbContext.OrderSignups
                .Where(x => x.OrderId == request.Id)
                .SelectMany(x => x.OrderItems)
                .Select(item => item.Dish)
                .AnyAsync(dish => dish.Blocked, cancellationToken);

            if (anyDishBlocked)
                throw new BlockedDishException("There is an excluded dish in your order");


            var order = await _orderService.GetBasicOrderInfo(request.Id, cancellationToken);

                order.IsActive = false;

                _dbContext.SaveChanges();

                return true;
        }
    }
}
