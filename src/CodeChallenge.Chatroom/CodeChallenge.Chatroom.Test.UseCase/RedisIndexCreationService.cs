using CodeChallenge.Chatroom.Infra.Data;
using Microsoft.Extensions.Hosting;
using Redis.OM;

namespace CodeChallenge.Chatroom.Test.UseCase
{
    public class RedisIndexCreationService : IHostedService
    {
        private readonly RedisConnectionProvider provider;
        public RedisIndexCreationService(RedisConnectionProvider provider)
        {
            this.provider = provider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await provider.Connection.CreateIndexAsync(typeof(RedisRoom));
            await provider.Connection.CreateIndexAsync(typeof(RedisMessage));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}