namespace CodeChallenge.Chatroom.Core
{
    public class MessageSendingService
    {
        private readonly IRoomRepository roomRepository;
        private readonly IEnumerable<IChatbotService> chatbotServices;

        public MessageSendingService(
            IRoomRepository roomRepository,
            IEnumerable<IChatbotService> chatbotServices)
        {
            this.roomRepository = roomRepository;
            this.chatbotServices = chatbotServices;
        }

        public async Task Send(string content, Guid userId)
        {
            var room = await roomRepository.GetOfUserId(userId);
            var message = new Message(content, userId);

            await Send(message, room);
        }

        public async Task Quote(Guid messageId, Room room, string quoteContent, Guid botId)
        {
            var message = new Message(quoteContent, botId, messageId);

            await Send(message, room);
        }

        private async Task Send(Message message, Room? room)
        {
            if (room == null)
            {
                throw new InvalidOperationException("This user has not joined the message room");
            }

            if (message.IsCommand)
            {
                foreach (var chatbot in chatbotServices)
                {
                    var awnser = await chatbot.Awnser(message, room);

                    if (awnser != null)
                    {
                        room.AddMessage(awnser);
                    }
                }
            }
            else
            {
                room.AddMessage(message);
            }
            
            await roomRepository.Put(room);
        }
    }
}
