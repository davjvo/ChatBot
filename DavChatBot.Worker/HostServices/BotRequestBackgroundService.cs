using DatChatBot.DataLayer.Records;
using DavChatBot.Services.RabbitMqServices;
using Microsoft.Extensions.Options;
using DatChatBot.DataLayer;
using DatChatBot.DataLayer.Constants;
using DatChatBot.DataLayer.Entities;
using DavChatBot.Services.ChatServices;
using Microsoft.EntityFrameworkCore;

namespace DavChatBot.Worker.HostServices
{

    public class BotRequestBackgroundService : IHostedService
    {
        private readonly StockOptions _stockOptions;
        private readonly IRabbitMqService _rabbitMqService;
        private readonly IStockService _stockService;
        private readonly DavChatBotDbContext _dbContextFactory;

        public BotRequestBackgroundService(
            IOptions<StockOptions> stockApiOptions,
            IRabbitMqService rabbitMqService,
            IDbContextFactory<DavChatBotDbContext> dbContext,
            IStockService stockService)
        {
            _stockOptions = stockApiOptions.Value;
            _rabbitMqService = rabbitMqService;
            _dbContextFactory = dbContext.CreateDbContext();

            if (string.IsNullOrWhiteSpace(_stockOptions.Url))
                throw new ArgumentNullException(nameof(_stockOptions.Url));

            _stockService = stockService;
        }

        private async Task ResolveStockCode(CommandInformation command, CancellationToken ct = default)
        {
            var message = command.Command switch
            {
                "/stock" => await _stockService.GetStockMessage(_stockOptions.Url, command.Parameter, ct),
                _ => $"CommandInfo {command} is not a Stock Command"
            };

            await ProduceBotMessage(message, ct);
        }

        public async Task ProduceBotMessage(string message, CancellationToken ct = default)
        {
            var chatMessage = new ChatBotMessage
            {
                CreatedAt = DateTimeOffset.Now,
                Message = message,
                UserId = BotConstants.Id,
            };

            await _rabbitMqService.Produce("chat-queue", chatMessage, ct);
        }

        public async Task StartAsync(CancellationToken ct) => await _rabbitMqService.Consume<CommandInformation>("", ResolveStockCode, ct);

        public Task StopAsync(CancellationToken ct)
        {
            _rabbitMqService.Dispose();
            return Task.CompletedTask;
        }
    }
}