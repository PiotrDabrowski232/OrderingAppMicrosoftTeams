using AutoMapper;
using MediatR;
using OrderingApp.Data.DBConfig;

namespace OrderingApp.Logic.Functions
{
    public abstract class BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        protected readonly OrderingDbContext _dbContext;
        protected readonly IMapper _mapper;

        protected BaseRequestHandler(OrderingDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
