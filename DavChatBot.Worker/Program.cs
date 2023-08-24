using Microsoft.EntityFrameworkCore;
using DatChatBot.DataLayer;
using DavChatBot.Services.RabbitMqServices;
using DavChatBot.Worker;
using DavChatBot.Worker.HostServices;
using DavChatBot.Services.ChatServices;
using DavChatBot.Services.UserServices;
using DatChatBot.DataLayer.Entities;
using Microsoft.AspNetCore.Identity;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHttpClient();


        var connectionString = hostContext.Configuration.GetConnectionString("Default");
        services.AddDbContextFactory<DavChatBotDbContext>(options => options.UseSqlServer(connectionString));

        services.Configure<StockOptions>(hostContext.Configuration.GetRequiredSection(StockOptions.STOCK_API_OPTIONS));

        services.AddTransient<IStockService, StockService>();
        services.AddTransient<IUserService, UserService>();
        services.AddSingleton<IRabbitMqService, RabbitMqService>();


        services.AddHostedService<BotRequestBackgroundService>();
    })
    .Build();

await host.StartAsync();
await host.WaitForShutdownAsync();