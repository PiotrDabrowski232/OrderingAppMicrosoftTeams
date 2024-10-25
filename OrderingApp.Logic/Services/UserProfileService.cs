﻿using Azure.Core;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.TeamsFx;
using OrderingApp.Logic.DTO;
using OrderingApp.Logic.Services.Interface;
using OrderingApp.Shared.Exceptions;

namespace OrderingApp.Logic.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly TeamsUserCredential _teamsUserCredential;
        private readonly IConfiguration _configuration;
        private static readonly string[] _scopes = { "User.Read", "User.ReadBasic.All", "ChatMessage.Send", "Chat.ReadWrite", "Group.Read.All", "Channel.ReadBasic.All", "Team.ReadBasic.All" };
        private const string appId = "d497c55c-bda3-473e-bcf6-d5dbe5bafd2d";
        private GraphServiceClient? _graphClient;

        public UserProfileService(TeamsUserCredential teamsUserCredential, IConfiguration configuration)
        {
            _teamsUserCredential = teamsUserCredential;
            _configuration = configuration;
        }

        private async Task<GraphServiceClient> GetGraphClientAsync()
        {
            if (_graphClient == null)
            {
                var tokenCredential = await GetOnBehalfOfCredential();
                _graphClient = new GraphServiceClient(tokenCredential, _scopes);
            }
            return _graphClient;
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
            var graphClient = await GetGraphClientAsync();
            return await graphClient.Me.GetAsync();
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var graphClient = await GetGraphClientAsync();

            try
            {
                var users = await graphClient.Users
                    .GetAsync(requestConfiguration =>
                    {
                        requestConfiguration.QueryParameters.Select = new[] { "id", "displayName"};
                    });

                return users.Value;

            }
            catch (ServiceException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task SendMessage(CommentDto comment)
        {
            var graphClient = await GetGraphClientAsync();
            var chatId = await GetChatAsync(comment.UserId.ToString());

            var mentionId = 0;
            var mention = new ChatMessageMention
            {
                Id = mentionId, 
                MentionText = $"@{comment.UserDisplayname}", 
                Mentioned = new ChatMessageMentionedIdentitySet
                {
                    User = new Identity
                    {
                        Id = comment.UserId.ToString() 
                    }
                },
                OdataType = "#microsoft.graph.chatMessageMention" 
            };

            var chatMessage = new ChatMessage
            {
                Body = new ItemBody
                {
                    ContentType = BodyType.Html,
                    Content = $"<h1 style=\"color:white; background-color:black; text-decoration:underline; padding: 10px;\">Message Generated By OrderingAPP</h1></br>" +
                              $"<at id=\"{mentionId}\">{mention.MentionText}</at> {comment.Comment} <br/>" +
                              $"<p style=\"color:black; background-color:white; padding: 10px; margin-top: 10px;\">Order Name: <strong>{comment.OrderName}</strong></p>"
                },
                Mentions = new List<ChatMessageMention> { mention }
            };


            await graphClient.Chats[chatId].Messages.PostAsync(chatMessage);
        }

        private async Task<string?> GetChatAsync(string userId)
        {
            var myData = await GetUserProfileAsync();

            var chat = myData.Chats?.FirstOrDefault(x => x.Members.Any(x => x.Id == userId));

            if (chat != null)
                return chat.Id;
            else
                return await CreateChatWithUserAsync(userId);
        }

        private async Task<string> CreateChatWithUserAsync(string userId)
        {
            var graphClient = await GetGraphClientAsync();

            var myId = await graphClient.Me.GetAsync();

            var requestBody = new Chat
            {
                ChatType = ChatType.OneOnOne,
                Members = new List<ConversationMember>
                {
                    new AadUserConversationMember
                    {
                        OdataType = "#microsoft.graph.aadUserConversationMember",
                        Roles = new List<string>
                        {
                            "owner",
                        },
                        AdditionalData = new Dictionary<string, object>
                        {
                            {
                                "user@odata.bind" , $"https://graph.microsoft.com/v1.0/users('{myId.Id}')"
                            },
                        },
                    },
                    new AadUserConversationMember
                    {
                        OdataType = "#microsoft.graph.aadUserConversationMember",
                        Roles = new List<string>
                        {
                            "owner",
                        },
                        AdditionalData = new Dictionary<string, object>
                        {
                            {
                                "user@odata.bind" , $"https://graph.microsoft.com/v1.0/users('{userId}')"
                            },
                        },
                    }
                }
            };

            var result = await graphClient.Chats.PostAsync(requestBody);

            return result.Id;
        }
    }
}
