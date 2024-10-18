using Microsoft.Graph.Models;
using OrderingApp.Logic.DTO;

namespace OrderingApp.Logic.Services.Interface
{
    public interface IUserProfileService
    {
        public Task<User> GetUserProfileAsync();
        public Task<Guid> GetUserProfileIdAsync();
        public Task<string> GetUserProfileNameAsync();
        public Task<List<User>> GetAllUsersAsync();
        public Task SendMessage(CommentDto comment);

    }
}
