using MediatR;
using OrderingApp.Data.Models.Enum;

namespace OrderingApp.Restaurant.Logic.Functions.Query
{
    public class GetRestaurantTypesQuery : IRequest<List<string>>;

    public class GetRestaurantTypesQueryHandler : IRequestHandler<GetRestaurantTypesQuery, List<string>>
    {
        public async Task<List<string>> Handle(GetRestaurantTypesQuery request, CancellationToken cancellationToken)
        {
            return Enum.GetNames(typeof(RestauranType)).ToList();
        }
    }
}
