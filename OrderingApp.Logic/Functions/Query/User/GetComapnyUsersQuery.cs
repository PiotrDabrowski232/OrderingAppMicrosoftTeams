using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingApp.Data.DBConfig;
using OrderingApp.Logic.Services.Interface;

namespace OrderingApp.Logic.Functions.Query.User
{
    public class GetComapnyUsersQuery(Guid id) : IRequest<IDictionary<Guid, string>>
    {
        public Guid Id { get; set; } = id;
    }

    public class GetComapnyUsersQueryHandler : BaseRequestHandler<GetComapnyUsersQuery, IDictionary<Guid, string>>
    {
        private readonly IUserProfileService _userProfileService;
        public GetComapnyUsersQueryHandler(OrderingDbContext dbContext, IMapper mapper,
            IUserProfileService userProfileService) : base(dbContext, mapper)
        {
            _userProfileService = userProfileService;
        }

        public override async Task<IDictionary<Guid, string>> Handle(GetComapnyUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userProfileService.GetAllUsersAsync();

            if (users == null || !users.Any())
            {
                return new Dictionary<Guid, string>();
            }

            var orderSignups = await _dbContext.OrderSignups
                .Where(x => x.OrderId == request.Id)
                .Select(x => x.SignedUser)
                .ToListAsync(cancellationToken);

            var result = users
                .Where(u => !orderSignups.Contains(Guid.Parse(u.Id))) 
                .ToDictionary(x => Guid.Parse(x.Id), x => x.DisplayName);

            return result;
        }
    }
}
