using Microsoft.EntityFrameworkCore;
using DatChatBot.DataLayer;
using DavChatBot.Services.RabbitMqServices;
using DavChatBot.Worker;
using DavChatBot.Worker.HostServices;
using DavChatBot.Services.ChatServices;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHttpClient();

        services.AddTransient<IStockService, StockService>();

        var connectionString = hostContext.Configuration.GetConnectionString("Default");
        services.AddDbContextFactory<DavChatBotDbContext>(options => options.UseSqlServer(connectionString));

        services.AddSingleton<IRabbitMqService, RabbitMqService>();

        services.Configure<StockOptions>(hostContext.Configuration.GetRequiredSection(StockOptions.STOCK_API_OPTIONS));

        services.AddHostedService<BotRequestBackgroundService>();
    })
    .Build();

await host.StartAsync();
await host.WaitForShutdownAsync();