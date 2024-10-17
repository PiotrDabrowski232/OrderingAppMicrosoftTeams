using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingApp.Data.DBConfig;
using OrderingApp.Logic.DTO;

namespace OrderingApp.Logic.Functions.Command.OrderItems
{
    public class UpdateOrderItemsCommand(Guid signupId, List<OrderItemsDto> orderItems) : IRequest<Guid>
    {
        public Guid SignupId { get; set; } = signupId;
        public List<OrderItemsDto> OrderItems { get; set; } = orderItems;
    }

    public class UpdateOrderItemsCommandHandler : BaseRequestHandler<UpdateOrderItemsCommand, Guid>
    {
        public UpdateOrderItemsCommandHandler(OrderingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<Guid> Handle(UpdateOrderItemsCommand request, CancellationToken cancellationToken)
        {
            var orderItems = await _dbContext.OrderSignups
                .Where(x => x.Id == request.SignupId)
                .Include(x => x.OrderItems)
                .FirstOrDefaultAsync(cancellationToken);

            if (orderItems != null && orderItems.OrderItems.Count != 0)
                _dbContext.OrderItems.RemoveRange(orderItems.OrderItems);

            var mappedItems = _mapper.Map<List<Data.Models.OrderItems>>(request.OrderItems, opt => opt.Items["SignupId"] = request.SignupId);

            _dbContext.OrderItems.AddRange(mappedItems);

            _dbContext.SaveChanges();

            return orderItems.Id;
        }
    }
}
