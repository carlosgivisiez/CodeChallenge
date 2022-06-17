using CodeChallenge.Chatroom.Core;
using Redis.OM;
using Redis.OM.Searching;

namespace CodeChallenge.Chatroom.Infra.Data
{
    public class RoomRepository : IRoomRepository
    {
        private readonly RedisCollection<RedisRoom> redisRooms;

        public RoomRepository(RedisConnectionProvider provider)
        {
            redisRooms = (RedisCollection<RedisRoom>)provider.RedisCollection<RedisRoom>();
        }

        public async Task Delete(Room room)
        {
            await redisRooms.Delete(room);
        }

        public async Task<Room?> Get(Guid id)
        {
            return await redisRooms.FindByIdAsync(id.ToString());
        }

        public async Task<Room?> GetOfMessageId(Guid messageId, Guid userId)
        {
            var allRooms = await redisRooms
                .ToListAsync();

            return allRooms
                .Where(r => r.Messages.Any(m => m.Id == messageId))
                .Where(r => r.MemberIds.Contains(userId))
                .FirstOrDefault();
        }

        public async Task<Room?> GetOfUserId(Guid userId)
        {
            var allRooms = await redisRooms
                .ToListAsync();

            return allRooms.FirstOrDefault(r => r.MemberIds.Contains(userId));
        }

        public async Task Put(Room room)
        {
            await redisRooms.InsertAsync(room);
        }
    }
}
