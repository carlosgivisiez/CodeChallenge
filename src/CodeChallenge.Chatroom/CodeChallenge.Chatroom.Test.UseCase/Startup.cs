using CodeChallenge.Chatroom.Core;
using CodeChallenge.Chatroom.Infra.Data;
using Microsoft.Extensions.DependencyInjection;
using Redis.OM;

namespace CodeChallenge.Chatroom.Test.UseCase
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHostedService<RedisIndexCreationService>();
            services.AddSingleton(new RedisConnectionProvider("redis://default:redispw@localhost:6379"));

            services.AddTransient<IRoomRepository, RoomRepository>();
            services.AddTransient<IRoomQueryService, RoomQueryService>();

            services.AddTransient<RoomCreatingService>();
            services.AddTransient<RoomJoiningService>();
            services.AddTransient<RoomLeavingService>();
            services.AddTransient<MessageSendingService>();
        }
    }
}
