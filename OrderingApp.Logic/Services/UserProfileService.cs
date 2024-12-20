﻿using Azure.Core;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.TeamsFx;
using OrderingApp.Data.Models;
using OrderingApp.Logic.DTO;
using OrderingApp.Logic.Services.Interface;
using OrderingApp.Shared.Exceptions;

namespace OrderingApp.Logic.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly TeamsUserCredential _teamsUserCredential;
        private readonly IConfiguration _configuration;
        private static readonly string[] _scopes = { "TeamsAppInstallation.ReadForUser", "User.Read.All", "TeamsAppInstallation.ReadForTeam", "User.Read", "User.ReadBasic.All", "ChatMessage.Send", "Chat.ReadWrite", "Group.Read.All", "Channel.ReadBasic.All", "Team.ReadBasic.All", "AppCatalog.Read.All", "TeamsTab.Read.All", "Application.Read.All" };
        private GraphServiceClient? _graphClient;
        private readonly ILogger<UserProfileService> _logger;

        public UserProfileService(TeamsUserCredential teamsUserCredential, IConfiguration configuration, ILogger<UserProfileService> logger)
        {
            _teamsUserCredential = teamsUserCredential;
            _configuration = configuration;
            _logger = logger;
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
            return user.DisplayName;
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

        public async Task<string> GetUserProfileNameAsync(Guid userId)
        {
            var user = await GetUserProfileAsync(userId);
            return user.DisplayName;
        }

        public async Task<User> GetUserProfileAsync(Guid userId)
        {
            var graphClient = await GetGraphClientAsync();
            return await graphClient.Users[userId.ToString()].GetAsync();
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var graphClient = await GetGraphClientAsync();

            try
            {
                var users = await graphClient.Users
                    .GetAsync(requestConfiguration =>
                    {
                        requestConfiguration.QueryParameters.Select = new[] { "id", "displayName" };
                    });

                return users.Value;

            }
            catch (ServiceException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task SendMessage(CommentDto comment, AppDataDto appData)
        {
            var graphClient = await GetGraphClientAsync();
            var chatId = await GetChatAsync(comment.UserId.ToString());

            string deeplink = "";

            if (appData != null)
            {
                var tab = await GetChannelTabsAsync(appData);
                deeplink = tab.WebUrl;
            }

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

            if (!string.IsNullOrEmpty(deeplink))
            {
                chatMessage.Body.Content += $"<p><a href=\"{deeplink}\" style=\"color:blue; text-decoration:underline;\">Click here to open in Teams</a></p>";
            }

            await graphClient.Chats[chatId].Messages.PostAsync(chatMessage);
        }

        public async Task SendMessage(string ordername, IDictionary<Guid, string> participants, AppDataDto appData)
        {
            var graphClient = await GetGraphClientAsync();

            string deeplink = string.Empty;
            if (appData != null)
            {
                var tab = await GetChannelTabsAsync(appData);
                deeplink = tab.WebUrl;
            }

            var sendTasks = new List<Task>();

            foreach (var participant in participants)
            {
                var participantKey = participant.Key.ToString();
                var participantName = participant.Value;

                var chatIdTask = GetChatAsync(participantKey);

                var mention = new ChatMessageMention
                {
                    Id = 0,  
                    MentionText = $"@{participantName}",
                    Mentioned = new ChatMessageMentionedIdentitySet
                    {
                        User = new Identity { Id = participantKey }
                    },
                    OdataType = "#microsoft.graph.chatMessageMention"
                };

                var content = $"<h1 style=\"color:white; background-color:black; text-decoration:underline; padding: 10px;\">Message Generated By OrderingAPP</h1></br>" +
                              $"<at id=\"0\">{mention.MentionText}</at> Order is waiting for your payment <br/>" +
                              $"<p style=\"color:black; background-color:white; padding: 10px; margin-top: 10px;\">Order Name: <strong>{ordername}</strong></p>";

                if (!string.IsNullOrEmpty(deeplink))
                {
                    content += $"<p><a href=\"{deeplink}\" style=\"color:blue; text-decoration:underline;\">Click here to open in Teams</a></p>";
                }

                var chatMessage = new ChatMessage
                {
                    Body = new ItemBody
                    {
                        ContentType = BodyType.Html,
                        Content = content
                    },
                    Mentions = new List<ChatMessageMention> { mention }
                };

                sendTasks.Add(chatIdTask.ContinueWith(async chatId =>
                    await graphClient.Chats[chatId.Result].Messages.PostAsync(chatMessage)
                ).Unwrap());
            }

            await Task.WhenAll(sendTasks);
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
        
        private async Task<TeamsTab> GetChannelTabsAsync(AppDataDto appData)
        {
            var graphClient = await GetGraphClientAsync();
            var tabs = await graphClient.Teams[appData.TeamId].Channels[appData.ChannelId].Tabs.GetAsync();
            return tabs.Value.FirstOrDefault(x => x.Configuration.EntityId == appData.EntityId);
        }
    }
}
