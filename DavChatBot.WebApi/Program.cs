using DatChatBot.DataLayer;
using DatChatBot.DataLayer.Entities;
using DavChatBot.Services.UserServices;
using DavChatBot.WebApi.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .SetIsOriginAllowed((host) => true)
               .AllowAnyHeader();
    });
    options.AddPolicy("AllowSpecificOrigins", builder =>
    {
        builder
            .WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        builder
            .WithOrigins("https://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

builder.Services.AddDbContext<DavChatBotDbContext>(options =>
{
    options.EnableDetailedErrors();
    var connectionString = builder.Configuration.GetConnectionString("Default");
    options.UseSqlServer(connectionString);
});

builder.Services
    .AddIdentity<User, IdentityRole<int>>(options =>
    {
        options.User.RequireUniqueEmail = true;
        options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    })
    .AddEntityFrameworkStores<DavChatBotDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/chathub");


app.Run();

