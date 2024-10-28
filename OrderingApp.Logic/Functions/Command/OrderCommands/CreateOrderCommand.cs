using AutoMapper;
using MediatR;
using OrderingApp.Data.DBConfig;
using OrderingApp.Data.Models;
using OrderingApp.Logic.DTO;
using OrderingApp.Logic.Services.Interface;
using OrderingApp.Shared.Exceptions;

namespace OrderingApp.Logic.Functions.Command.OrderCommands
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public OrderDto Order { get; set; }
        public Guid Id { get; set; }
        public CreateOrderCommand(OrderDto order)
        {
            Order = order;
        }
        public CreateOrderCommand(OrderDto order, Guid id)
        {
            Order = order;
            Id = id;
        }

    }

    public class CreateOrderCommandHandler : BaseRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderService _orderService;
        private readonly IUserProfileService _userProfileService;

        public CreateOrderCommandHandler(OrderingDbContext dbContext, IMapper mapper,
            IOrderService orderService, IUserProfileService userProfileService) : base(dbContext, mapper)
        {
            _orderService = orderService;
            _userProfileService = userProfileService;
        }

        public override async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var order = _mapper.Map<Order>(request.Order);

                var existOrder = await _orderService.GetBasicOrderInfo(request.Id, cancellationToken);

                var orderNameExist = await _orderService.OrderNameExist(order.Name, request.Id, cancellationToken);

                if (orderNameExist)
                    throw new EntityExistException("There is Active Order Named this way");


                if (existOrder != null)
                {
                    existOrder.Name = order.Name;
                    existOrder.RestaurantId = order.RestaurantId;
                    existOrder.MinValue = order.MinValue;
                    existOrder.DeliveryCost = order.DeliveryCost;
                    existOrder.FreeDeliveryFrom = order.FreeDeliveryFrom;
                    existOrder.PhoneNumber = order.PhoneNumber;
                    existOrder.BankAccountNumber = order.BankAccountNumber;

                    await _dbContext.SaveChangesAsync();

                }
                else
                {

                    order.CreatedBy = await _userProfileService.GetUserProfileIdAsync();

                    await _dbContext.Orders.AddAsync(order, cancellationToken);
                    await _dbContext.SaveChangesAsync(cancellationToken);

                }
                return order.Id;
            }
            catch
            {
                throw;
            }
        }
    }
}
