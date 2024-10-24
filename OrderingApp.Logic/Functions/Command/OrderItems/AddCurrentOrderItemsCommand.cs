using AutoMapper;
using MediatR;
using OrderingApp.Data.DBConfig;
using OrderingApp.Data.Models;
using OrderingApp.Logic.DTO;
using OrderingApp.Logic.Services.Interface;

namespace OrderingApp.Logic.Functions.Command.OrderItems
{
    public class AddCurrentOrderItemsCommand(Guid orderId, List<OrderItemsDto> orderItems) : IRequest<Guid>
    {
        public Guid OrderId { get; set; } = orderId;
        public List<OrderItemsDto> OrderItems { get; set; } = orderItems;
    }

    public class AddCurrentOrderItemsCommandHandler : BaseRequestHandler<AddCurrentOrderItemsCommand, Guid>
    {
        private IUserProfileService _userProfileService;
        public AddCurrentOrderItemsCommandHandler(OrderingDbContext dbContext, IMapper mapper,
            IUserProfileService userProfileService) : base(dbContext, mapper)
        {
            _userProfileService = userProfileService;
        }

        public override async Task<Guid> Handle(AddCurrentOrderItemsCommand request, CancellationToken cancellationToken)
        {
            var userData = await _userProfileService.GetUserProfileAsync();

            var signup = new OrderSignups
            {
                Id = Guid.NewGuid(),
                SignedUser = Guid.Parse(userData.Id),
                UserDisplayName = userData.DisplayName,
                OrderId = request.OrderId,
            };

            _dbContext.OrderSignups.Add(signup);

            var mappedOrderItems = _mapper.Map<List<Data.Models.OrderItems>>(
                request.OrderItems, opt => opt.Items["SignupId"] = signup.Id);

            _dbContext.OrderItems.AddRange(mappedOrderItems);

            _dbContext.SaveChanges();

            return request.OrderId;
        }
    }
}
