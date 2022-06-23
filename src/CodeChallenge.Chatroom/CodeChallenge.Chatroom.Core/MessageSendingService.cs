namespace CodeChallenge.Chatroom.Core
{
    public class MessageSendingService
    {
        private readonly IRoomRepository roomRepository;

        public MessageSendingService(IRoomRepository roomRepository)
        {
            this.roomRepository = roomRepository;
        }

        public async Task Send(string content, Guid userId)
        {
            var room = await roomRepository.GetOfUserId(userId);
            var message = new Message(content, userId);

            await Send(message, room);
        }

        public async Task Quote(Guid messageId, string quoteContent, Guid userId)
        {
            var room = await roomRepository.GetOfMessageId(messageId, userId);
            var message = new Message(quoteContent, userId, messageId);

            await Send(message, room);
        }

        private async Task Send(Message message, Room? room)
        {
            if (room == null)
            {
                throw new InvalidOperationException("This user has not joined the message room");
            }

            room.AddMessage(message);

            await roomRepository.Put(room);
        }
    }
}
