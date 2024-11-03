using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingApp.Data.DBConfig;

namespace OrderingApp.Logic.Functions.Command.SignupCommands
{
    public class ChangePaymentStatusCommand(Guid signupId) : IRequest<Guid>
    {
        public Guid SignupId { get; set; } = signupId;
    }

    public class ChangePaymentStatusCommandHandler : BaseRequestHandler<ChangePaymentStatusCommand, Guid>
    {
        public ChangePaymentStatusCommandHandler(OrderingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<Guid> Handle(ChangePaymentStatusCommand request, CancellationToken cancellationToken)
        {
            var signup = await _dbContext.OrderSignups
                .FirstOrDefaultAsync(x => x.Id == request.SignupId, cancellationToken);

            signup.IsPaid = !signup.IsPaid;

            _dbContext.SaveChanges();

            return signup.OrderId;
        }
    }
}
