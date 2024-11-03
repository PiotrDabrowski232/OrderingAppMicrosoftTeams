using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingApp.Data.DBConfig;
using OrderingApp.Data.Models.Enum;
using OrderingApp.Logic.DTO;
using OrderingApp.Logic.Services.Interface;

namespace OrderingApp.Logic.Functions.Query.Order
{
    public class GetAllArchivedOrdersQuery : IRequest<IEnumerable<OrderDetailsDto>>;

    public class GetAllArchivedOrdersQueryHandler : BaseRequestHandler<GetAllArchivedOrdersQuery, IEnumerable<OrderDetailsDto>>
    {
        private readonly IUserProfileService _userProfileService;
        public GetAllArchivedOrdersQueryHandler(OrderingDbContext dbContext, IMapper mapper, IUserProfileService userProfileService) : base(dbContext, mapper)
        {
            _userProfileService = userProfileService;
        }

        public override async Task<IEnumerable<OrderDetailsDto>> Handle(GetAllArchivedOrdersQuery request, CancellationToken cancellationToken)
        {
            var userId = await _userProfileService.GetUserProfileIdAsync();

            return await _dbContext.Orders
                .Where(x => (x.CreatedBy == userId || x.OrderSignups.Any(x => x.SignedUser == userId)) && x.Status == OrderStatus.Closed)
                .Select(x => new OrderDetailsDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreationDate = x.CreationDate,
                    RestaurantName = x.Restaurant.Name,
                    RestaurantType = x.Restaurant.RestaurantType,
                }).OrderByDescending(x => x.CreationDate)
                .ToListAsync(cancellationToken);
        }
    }
}
