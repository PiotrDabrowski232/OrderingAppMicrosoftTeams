using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingApp.Data.DBConfig;
using OrderingApp.Shared.Exceptions;

namespace OrderingApp.Logic.Functions.Command.SignupCommands
{
    public class RemoveSignupCommand(Guid id) : IRequest<bool>
    {
        public Guid Id { get; set; } = id;
    }

    public class RemoveSignupCommandHandler : BaseRequestHandler<RemoveSignupCommand, bool>
    {
        public RemoveSignupCommandHandler(OrderingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<bool> Handle(RemoveSignupCommand request, CancellationToken cancellationToken)
        {
            var signup = await _dbContext.OrderSignups.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (signup != null)
                _dbContext.OrderSignups.Remove(signup);
            else
                throw new EntityNotFoundException("There aren't signup with provided id");

            _dbContext.SaveChanges();

            return true;
        }
    }
}
