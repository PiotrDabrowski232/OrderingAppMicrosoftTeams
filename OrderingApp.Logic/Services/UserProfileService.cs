using Azure.Core;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.TeamsFx;
using OrderingApp.Logic.Services.Interface;
using OrderingApp.Shared.Exceptions;

namespace OrderingApp.Logic.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly TeamsUserCredential _teamsUserCredential;
        private readonly IConfiguration _configuration;
        private static readonly string[] _scopes = { "User.Read", "User.ReadBasic.All"};

        public UserProfileService(TeamsUserCredential teamsUserCredential, IConfiguration configuration)
        {
            _teamsUserCredential = teamsUserCredential;
            _configuration = configuration;
        }

        public async Task<string> GetUserProfileNameAsync()
        {
            var user = await GetUserProfileAsync();
            return user.DisplayName ?? throw new WrongProfileCredentialsException();
        }

        public async Task<Guid> GetUserProfileIdAsync()
        {
            var user = await GetUserProfileAsync();
            if (Guid.TryParse(user.Id, out var id))
                return id;
            else
                throw new WrongProfileCredentialsException("We can't load profile credentials");
        }

        public async Task<User> GetUserProfileAsync()
        {
            var tokenCredential = await GetOnBehalfOfCredential();
            var graphClient = GetGraphServiceClient(tokenCredential);
            return await graphClient.Me.GetAsync();
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var tokenCredential = await GetOnBehalfOfCredential();
            var graphClient = GetGraphServiceClient(tokenCredential);

            try
            {
                var users = await graphClient.Users
                    .GetAsync(requestConfiguration =>
                    {
                        requestConfiguration.QueryParameters.Select = new[] { "id", "displayName", "userPrincipalName" };
                    });

                return users.Value;

            }
            catch (ServiceException ex)
            {
                throw new ApplicationException($"Błąd przy pobieraniu użytkowników: {ex.Message}", ex);
            }
        }

        private GraphServiceClient GetGraphServiceClient(TokenCredential tokenCredential)
        {
            return new GraphServiceClient(tokenCredential, _scopes);
        }

        private async Task<OnBehalfOfCredential> GetOnBehalfOfCredential()
        {
            var config = _configuration.Get<ConfigOptions>();
            var tenantId = config.TeamsFx.Authentication.OAuthAuthority.Replace("https://login.microsoftonline.com/", string.Empty);
            AccessToken ssoToken = await _teamsUserCredential.GetTokenAsync(new TokenRequestContext(null), new CancellationToken());

            return new OnBehalfOfCredential(
                tenantId,
                config.TeamsFx.Authentication.ClientId,
                config.TeamsFx.Authentication.ClientSecret,
                ssoToken.Token
            );
        }
    }
}
