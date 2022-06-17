using CodeChallenge.Chatroom.Core;
using Redis.OM.Modeling;

namespace CodeChallenge.Chatroom.Infra.Data
{
    [Document(StorageType = StorageType.Json)]
    public class RedisMessage
    {
        [RedisIdField]
        public Guid Id { get; set; }
        [Indexed]
        public string Content { get; set; } = "";
        [Indexed]
        public Guid OwnerId { get; set; }
        [Indexed]
        public DateTime DateTime { get; set; }
        [Indexed]
        public Guid? QuoteeMessageId { get; set; }

        public static implicit operator Message(RedisMessage redisMessage)
        {
            return new(redisMessage.Id, redisMessage.Content, redisMessage.OwnerId, redisMessage.DateTime, redisMessage.QuoteeMessageId);
        }

        public static implicit operator RedisMessage(Message message)
        {
            return new RedisMessage
            {
                Id = message.Id,
                Content = message.Content,
                OwnerId = message.OwnerId,
                DateTime = message.DateTime,
                QuoteeMessageId = message.QuoteeMessageId
            };
        }
    }
}
