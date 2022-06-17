using CodeChallenge.Chatroom.Core;
using Redis.OM;
using Redis.OM.Searching;

namespace CodeChallenge.Chatroom.Infra.Data
{
    public class RoomQueryService : IRoomQueryService
    {
        private readonly RedisCollection<RedisRoom> redisRooms;

        public RoomQueryService(RedisConnectionProvider provider)
        {
            redisRooms = (RedisCollection<RedisRoom>)provider.RedisCollection<RedisRoom>();
        }

        public async Task<Room?> Get(Guid id)
        {
            return await redisRooms.FindByIdAsync(id.ToString());
        }

        public async Task<IEnumerable<RoomSummary>> GetSummaries()
        {
            var rooms = await redisRooms.ToListAsync();

            return rooms.Select(r => (RoomSummary)r).ToList();
        }
    }
}
