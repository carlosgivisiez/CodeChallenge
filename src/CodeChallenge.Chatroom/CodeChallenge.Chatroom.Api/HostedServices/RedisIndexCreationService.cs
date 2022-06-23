using CodeChallenge.Chatroom.Infra.Data;
using Redis.OM;

namespace CodeChallenge.Chatroom.Api.HostedServices
{
    public class RedisIndexCreationService : IHostedService
    {
        private readonly RedisConnectionProvider provider;
        public RedisIndexCreationService(RedisConnectionProvider provider)
        {
            this.provider = provider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            provider.Connection.CreateIndexAsync(typeof(RedisRoom));
            provider.Connection.CreateIndexAsync(typeof(RedisMessage));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}