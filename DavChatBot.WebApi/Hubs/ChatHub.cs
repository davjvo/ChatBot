using DatChatBot.DataLayer.Constants;
using DavChatBot.Services.ChatServices;
using DavChatBot.Services.RabbitMqServices;
using Microsoft.AspNetCore.SignalR;

namespace DavChatBot.WebApi.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatMessageService _chatMessageService;
        private readonly IRabbitMqService _rabbitMqService;
        private readonly IBotCommandService _botCommandService;

        public ChatHub(IChatMessageService chatMessageService, IRabbitMqService rabbitMqService, IBotCommandService botCommandService)
        {
            _chatMessageService = chatMessageService;
            _rabbitMqService = rabbitMqService;
            _botCommandService = botCommandService;
        }

        public async Task SendMessage(string user, string message, int userId)
        {
            await _chatMessageService.AddChatMessage(message, userId);
            await Clients.All.SendAsync(HubConstants.ReceiveMessage, user, message, userId);

            if (_botCommandService.IsCommand(message))
            {
                var commandInfo = _botCommandService.GetCommandInformation(message);

                if (!string.IsNullOrEmpty(commandInfo.Error))
                {
                    await Clients.All.SendAsync(HubConstants.ReceiveMessage, BotConstants.Name, commandInfo.Error, BotConstants.Id);
                }
                else
                {
                    await _rabbitMqService.Produce(QueueNames.Bot, commandInfo);
                }
            }
        }
    }
}
