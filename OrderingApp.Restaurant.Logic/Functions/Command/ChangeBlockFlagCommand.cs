using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingApp.Data.DBConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingApp.Restaurant.Logic.Functions.Command
{
    public class ChangeBlockFlagCommand(string dishId) : IRequest<bool>
    {
        public string DishId { get; set; } = dishId;
    }

    public class ChangeBlockFlagCommandHandler : IRequestHandler<ChangeBlockFlagCommand, bool>
    {
        private readonly OrderingDbContext _dbContext;
        public ChangeBlockFlagCommandHandler(OrderingDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Handle(ChangeBlockFlagCommand request, CancellationToken cancellationToken)
        {
            var dish = await _dbContext.Dishes.FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.DishId));

            dish.Blocked = !dish.Blocked;

            _dbContext.SaveChanges();

            return true;
        }
    }
}
