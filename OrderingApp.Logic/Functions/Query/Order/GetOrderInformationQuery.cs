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
        private readonly IValueComputationService _valueComputationService;
        public GetOrderInformationQueryHandler(OrderingDbContext dbContext, IMapper mapper,
            IUserProfileService userProfileService, IValueComputationService valueComputationService) : base(dbContext, mapper)
        {
            _userProfileService = userProfileService;
            _valueComputationService = valueComputationService;
        }

        public override async Task<OrderInformationDto> Handle(GetOrderInformationQuery request, CancellationToken cancellationToken)
        {
            var userId = await _userProfileService.GetUserProfileIdAsync();

            var orderInformation = await _dbContext.Orders
                .Include(x => x.OrderSignups)
                .Include(x => x.Restaurant)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            var mappedOrderInfo = _mapper.Map<OrderInformationDto>(orderInformation, opts =>
            {
                opts.Items["UserId"] = userId;
            });

            mappedOrderInfo.Author = await _userProfileService.GetUserProfileNameAsync(orderInformation.CreatedBy);

            var currentUserId = await _userProfileService.GetUserProfileIdAsync();

            mappedOrderInfo.Signups = mappedOrderInfo.Signups.Select(x =>
            {
                x.IsMySignup = (x.SignedUser == currentUserId);
                x.SignupValue = _valueComputationService.CalculateSignupTotalPrice(x.Id);
                return x;
            }).ToList();

            return mappedOrderInfo;
        }
    }
}
