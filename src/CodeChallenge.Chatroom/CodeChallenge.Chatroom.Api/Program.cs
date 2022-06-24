using CodeChallenge.Chatroom.Api.HostedServices;
using CodeChallenge.Chatroom.Api.Hubs;
using CodeChallenge.Chatroom.Bot.StockMarket;
using CodeChallenge.Chatroom.Core;
using CodeChallenge.Chatroom.Infra.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Redis.OM;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<RedisIndexCreationService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        builder.Configuration.GetSection("Auth").Bind(options);

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];
                var path = context.HttpContext.Request.Path;

                if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hub"))
                {
                    context.Token = accessToken;
                }

                return Task.CompletedTask;
            }
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR(o => o.EnableDetailedErrors = true).AddNewtonsoftJsonProtocol();

builder.Services.AddSingleton<IUserIdProvider, UserIdProvider>();
builder.Services.AddSingleton(new RedisConnectionProvider(builder.Configuration["Redis:ConnectionString"]));

builder.Services.AddTransient<IRoomRepository, RoomRepository>();
builder.Services.AddTransient<IRoomQueryService, RoomQueryService>();
builder.Services.AddTransient<RoomCreatingService>();
builder.Services.AddTransient<RoomJoiningService>();
builder.Services.AddTransient<RoomLeavingService>();
builder.Services.AddTransient<MessageSendingService>();
builder.Services.AddTransient<IChatbotService, StockMarketService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHub<RoomMessagingHub>("/hub");

app.Run();
