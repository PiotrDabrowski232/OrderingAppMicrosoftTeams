using AutoMapper;
using MediatR;
using OrderingApp.Data.DBConfig;
using OrderingApp.Logic.DTO;
using OrderingApp.Logic.Services.Interface;

namespace OrderingApp.Logic.Functions.Command.Comment
{
    public class SendCommentCommand(CommentDto comment, Guid signupId, AppDataDto appData) : IRequest<Guid>
    {
        public CommentDto Comment { get; set; } = comment;
        public Guid SignupId { get; set; } = signupId;
        public AppDataDto AppData { get; set; } = appData;
    }

    public class SendCommentCommandHandler : BaseRequestHandler<SendCommentCommand, Guid>
    {
        private readonly IUserProfileService _userProfileService;
        public SendCommentCommandHandler(OrderingDbContext dbContext, IMapper mapper, IUserProfileService userProfileService) : base(dbContext, mapper)
        {
            _userProfileService = userProfileService;
        }

        public override async Task<Guid> Handle(SendCommentCommand request, CancellationToken cancellationToken)
        {
            await _userProfileService.SendMessage(request.Comment, request.AppData);

            var comment = new Data.Models.Comment
            {
                Id = Guid.NewGuid(),
                Description = request.Comment.Comment,
                Author = await _userProfileService.GetUserProfileNameAsync(),
                Date = DateTime.Now,
                SignupId = request.SignupId,
            };

            _dbContext.Comments.Add(comment);

            _dbContext.SaveChanges();
            return comment.Id;
        }
    }
}
