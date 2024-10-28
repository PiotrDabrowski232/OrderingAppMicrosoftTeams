using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingApp.Data.DBConfig;
using OrderingApp.Logic.DTO;
using OrderingApp.Logic.Services.Interface;

namespace OrderingApp.Logic.Functions.Query.Order
{
    public class GetOrderInformationQuery : IRequest<OrderInformationDto>
    {
        public Guid Id { get; set; }
        public GetOrderInformationQuery(Guid id) => Id = id;
    }

    public class GetOrderInformationQueryHandler : BaseRequestHandler<GetOrderInformationQuery, OrderInformationDto>
    {
        private readonly IUserProfileService _userProfileService;
        public GetOrderInformationQueryHandler(OrderingDbContext dbContext, IMapper mapper, IUserProfileService userProfileService) : base(dbContext, mapper)
        {
            _userProfileService = userProfileService;
        }

        public override async Task<OrderInformationDto> Handle(GetOrderInformationQuery request, CancellationToken cancellationToken)
        {
            var userId = await _userProfileService.GetUserProfileIdAsync();

            var orderInformation = await _dbContext.Orders
                .Include(x => x.OrderSignups)
                .Include(x => x.Restaurant)
                .FirstOrDefaultAsync(x => x.Id == request.Id ,cancellationToken);

            var mappedOrderInfo = _mapper.Map<OrderInformationDto>(orderInformation, opts =>
            {
                opts.Items["UserId"] = userId;
            });
            
            mappedOrderInfo.Author = await _userProfileService.GetUserProfileNameAsync();

            var curentUserId = await _userProfileService.GetUserProfileIdAsync();

            foreach (var x in mappedOrderInfo.Signups)
            {
                if (x.SignedUser == curentUserId)
                    x.IsMySignup = true;
                else
                    x.IsMySignup = false;
            }

            return mappedOrderInfo ?? throw new Exception();
        }
    }
}
