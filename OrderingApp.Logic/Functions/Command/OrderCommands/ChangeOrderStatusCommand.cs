using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingApp.Data.DBConfig;
using OrderingApp.Logic.DTO;
using OrderingApp.Logic.Services.Interface;
using OrderingApp.Shared.Exceptions;

namespace OrderingApp.Logic.Functions.Command.OrderCommands
{
    public class ChangeOrderStatusCommand(Guid orderId, bool upgrade, AppDataDto appData) : IRequest<Guid>
    {
        public Guid OrderId { get; set; } = orderId;
        public bool Upgrade { get; set; } = upgrade;
        public AppDataDto AppData { get; set; } = appData;
    }

    public class ChangeOrderStatusCommandHandler : BaseRequestHandler<ChangeOrderStatusCommand, Guid>
    {
        private readonly IUserProfileService _userProfileService;
        public ChangeOrderStatusCommandHandler(OrderingDbContext dbContext, IMapper mapper, IUserProfileService userProfileService) : base(dbContext, mapper)
        {
            _userProfileService = userProfileService;
        }

        public override async Task<Guid> Handle(ChangeOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var anyDishBlocked = await _dbContext.OrderSignups
                .Where(x => x.OrderId == request.OrderId)
                .SelectMany(x => x.OrderItems)
                .Select(item => item.Dish)
                .AnyAsync(dish => dish.Blocked, cancellationToken);

            if (anyDishBlocked)
                throw new BlockedDishException("There is an excluded dish in your order");

            var order = await _dbContext.Orders
                .FirstOrDefaultAsync(x => x.Id == request.OrderId, cancellationToken);

            if (request.Upgrade)
                order.Status ++;
            else
                order.Status--;

            _dbContext.SaveChanges();

            if(order.Status == Data.Models.Enum.OrderStatus.Progres)
            {
                var signups = await _dbContext.OrderSignups
                .Where(x => x.OrderId == request.OrderId
                    && !x.IsPaid
                    && x.SignedUser != x.Order.CreatedBy)
                .ToDictionaryAsync(key => key.SignedUser,
                    value => value.UserDisplayName,
                    cancellationToken);

                await _userProfileService.SendMessage(order.Name, signups, request.AppData);
            }

            return order.Id;
        }
    }

}
