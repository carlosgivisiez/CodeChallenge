using CodeChallenge.Chatroom.Core;
using Redis.OM.Modeling;

namespace CodeChallenge.Chatroom.Infra.Data
{
    [Document(StorageType = StorageType.Json)]
    public class RedisRoom
    {
        [RedisIdField]
        public Guid Id { get; set; }
        [Indexed]
        public string Name { get; set; } = "";
        [Indexed]
        public ICollection<Guid> MemberIds { get; set; } = new HashSet<Guid>();
        [Indexed(CascadeDepth = 1)]
        public ICollection<RedisMessage> Messages { get; set; } = new HashSet<RedisMessage>();

        public static implicit operator Room?(RedisRoom? redisRoom)
        {
            if (redisRoom == null)
            {
                return null;
            }

            return new(redisRoom.Id, redisRoom.Name, redisRoom.MemberIds, redisRoom.Messages.Select(m => (Message)m).ToList());
        }

        public static implicit operator RedisRoom(Room room)
        {
            return new RedisRoom
            {
                Id = room.Id,
                Name = room.Name,
                MemberIds = room.MemberIds,
                Messages = room.Messages.Select(m => (RedisMessage)m).ToList()
            };
        }

        public static implicit operator RoomSummary(RedisRoom room) => new(room.Id, room.Name, room.MemberIds.Count);
    }
}
