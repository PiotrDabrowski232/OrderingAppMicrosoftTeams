using Microsoft.Graph.Models;
using OrderingApp.Logic.DTO;

namespace OrderingApp.Logic.Services.Interface
{
    public interface IUserProfileService
    {
        public Task<User> GetUserProfileAsync();
        public Task<User> GetUserProfileAsync(Guid userId);
        public Task<Guid> GetUserProfileIdAsync();
        public Task<string> GetUserProfileNameAsync();
        public  Task<string> GetUserProfileNameAsync(Guid userId);
        public Task<List<User>> GetAllUsersAsync();
        public Task SendMessage(CommentDto comment, AppDataDto appData);
        public Task SendMessage(string ordername, IDictionary<Guid, string> participants, AppDataDto appData);
    }
}
