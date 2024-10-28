using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingApp.Data.DBConfig;
using OrderingApp.Logic.DTO;

namespace OrderingApp.Logic.Functions.Query.Order
{
    public class DishesPreviewQuery(Guid orderId) : IRequest<List<OrderItemsDto>>
    {
        public Guid OrderId { get; set; } = orderId;
    }


    public class DishPreviewQueryHandler : BaseRequestHandler<DishesPreviewQuery, List<OrderItemsDto>>
    {
        public DishPreviewQueryHandler(OrderingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

        }

        public override async Task<List<OrderItemsDto>> Handle(DishesPreviewQuery request, CancellationToken cancellationToken)
        {
            var items = await _dbContext.OrderItems
                .Where(x => x.Signup.OrderId == request.OrderId)
                .Include(x => x.Dish)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var groupedItems = items
                .GroupBy(x => x.Dish)
                .Select(g => new OrderItemsDto
                {
                    Dish = _mapper.Map<DishDto>(g.Key),
                    Amount = g.Sum(x => x.Amount)
                })
                .ToList();

            return groupedItems;

        }
    }

}
 