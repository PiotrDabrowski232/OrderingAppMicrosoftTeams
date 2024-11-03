using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using OrderingApp.Data.DBConfig;
using OrderingApp.Logic.DTO;
using OrderingApp.Logic.Services.Interface;

namespace OrderingApp.Logic.Functions.Query.Order
{
    public class GetAllActiveOrdersQuery : IRequest<IEnumerable<OrderDetailsDto>>;

    public class GetAllActiveOrdersQueryHandler : BaseRequestHandler<GetAllActiveOrdersQuery, IEnumerable<OrderDetailsDto>>
    {
        private readonly IUserProfileService _userProfileService;
        private readonly Guid Id;
        public GetAllActiveOrdersQueryHandler(OrderingDbContext dbContext, IMapper mapper, IUserProfileService userProfileService) : base(dbContext, mapper)
        {
            _userProfileService = userProfileService;
        }

        public override async Task<IEnumerable<OrderDetailsDto>> Handle(GetAllActiveOrdersQuery request, CancellationToken cancellationToken)
        {
            var userProfileId = await _userProfileService.GetUserProfileIdAsync();

            var ordersQuery = await _dbContext.Orders
                .Where(x => x.Status == Data.Models.Enum.OrderStatus.Active 
                || (x.Status == Data.Models.Enum.OrderStatus.Progres && (x.OrderSignups.Any(x => x.SignedUser == userProfileId) || x.CreatedBy == userProfileId)))
                .Select(x => new OrderDetailsDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreationDate = x.CreationDate,
                    RestaurantName = x.Restaurant.Name,
                    RestaurantType = x.Restaurant.RestaurantType,
                    MyOrder = x.CreatedBy == userProfileId,
                    Status = x.Status,
                })
                .OrderByDescending(x => x.CreationDate)
                .ToListAsync(cancellationToken);

            return ordersQuery;

        }
    }
}
