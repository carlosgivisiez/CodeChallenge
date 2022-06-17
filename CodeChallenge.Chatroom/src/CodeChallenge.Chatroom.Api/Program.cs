using CodeChallenge.Chatroom.Api;
using CodeChallenge.Chatroom.Core;
using CodeChallenge.Chatroom.Infra.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Redis.OM;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
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

builder.Services.AddSingleton<IUserIdProvider, UserIdProvider>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ChatroomScope", policy =>
    {
        policy.RequireClaim("scope", "chatroom");
    });
});

builder.Services.AddSignalR(o => o.EnableDetailedErrors = true)
    .AddNewtonsoftJsonProtocol();

builder.Services.AddHostedService<RedisIndexCreationService>();
builder.Services.AddSingleton(new RedisConnectionProvider(builder.Configuration["REDIS_CONNECTION_STRING"]));

builder.Services.AddTransient<IRoomRepository, RoomRepository>();
builder.Services.AddTransient<IRoomQueryService, RoomQueryService>();

builder.Services.AddTransient<RoomCreatingService>();
builder.Services.AddTransient<RoomJoiningService>();
builder.Services.AddTransient<RoomLeavingService>();
builder.Services.AddTransient<MessageSendingService>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<RoomMessagingHub>("/hub"); 

app.Run();
