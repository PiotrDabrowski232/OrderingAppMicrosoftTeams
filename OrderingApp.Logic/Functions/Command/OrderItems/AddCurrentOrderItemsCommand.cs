using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingApp.Data.DBConfig;
using OrderingApp.Data.Models;
using OrderingApp.Logic.DTO;
using OrderingApp.Shared.Exceptions;

namespace OrderingApp.Logic.Functions.Command.OrderItems
{
    public class AddCurrentOrderItemsCommand(Guid orderId, OrderSignupsDto signup, List<OrderItemsDto> orderItems) : IRequest<Guid>
    {
        public Guid OrderId { get; set; } = orderId;
        public OrderSignupsDto Signup { get; set; } = signup;
        public List<OrderItemsDto> OrderItems { get; set; } = orderItems;
    }

    public class AddCurrentOrderItemsCommandHandler : BaseRequestHandler<AddCurrentOrderItemsCommand, Guid>
    {
        public AddCurrentOrderItemsCommandHandler(OrderingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<Guid> Handle(AddCurrentOrderItemsCommand request, CancellationToken cancellationToken)
        {
            if (await _dbContext.OrderSignups
                 .Where(x => x.OrderId == request.OrderId)
                 .AnyAsync(x => x.SignedUser == request.Signup.SignedUser, cancellationToken))
                throw new UserSignedToOrderException("This User is signed to this order");

            var signedMapped = _mapper.Map<OrderSignups>(request.Signup);

            signedMapped.OrderId = request.OrderId;

            _dbContext.OrderSignups.Add(signedMapped);

            var mappedOrderItems = _mapper.Map<List<Data.Models.OrderItems>>(
                request.OrderItems, opt => opt.Items["SignupId"] = signedMapped.Id);

            _dbContext.OrderItems.AddRange(mappedOrderItems);

            _dbContext.SaveChanges();

            return request.OrderId;
        }
    }
}
