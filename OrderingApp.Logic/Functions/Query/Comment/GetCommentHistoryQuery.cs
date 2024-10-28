using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderingApp.Data.DBConfig;
using OrderingApp.Logic.DTO;

namespace OrderingApp.Logic.Functions.Query.Comment
{
    public class GetCommentHistoryQuery(Guid signupId) : IRequest<List<CommentHistoryDto>>
    {
        public Guid SignupId { get; set; } = signupId;
    }

    public class GetCommentHistoryQueryHandler : BaseRequestHandler<GetCommentHistoryQuery, List<CommentHistoryDto>>
    {
        public GetCommentHistoryQueryHandler(OrderingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

        }

        public override async Task<List<CommentHistoryDto>> Handle(GetCommentHistoryQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Comments.Where(x => x.SignupId == request.SignupId).Select(x => new CommentHistoryDto
            {
                Id = x.Id,
                Description = x.Description,
                Author = x.Author,
                Date = x.Date,
            }).OrderByDescending(x => x.Date)
            .ToListAsync(cancellationToken);

        }
    }

}
