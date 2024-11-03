using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingApp.Data.DBConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingApp.Logic.Functions.Command.OrderCommands
{
    public class DeleteOrderCommand(Guid orderId) : IRequest<bool>
    {
        public Guid OrderId { get; set; } = orderId;
    }

    public class DeleteOrderCommandHandler : BaseRequestHandler<DeleteOrderCommand, bool>
    {
        public DeleteOrderCommandHandler(OrderingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _dbContext
                .Orders
                .Where(x => x.Id == request.OrderId)
                .Include(x => x.OrderSignups)
                .ThenInclude(x => x.OrderItems)
                .FirstOrDefaultAsync(cancellationToken);

            _dbContext.Orders.Remove(order);

            _dbContext.SaveChanges();

            return true;

        }
    }
}
