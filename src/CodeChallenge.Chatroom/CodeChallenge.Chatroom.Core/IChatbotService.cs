namespace CodeChallenge.Chatroom.Core
{
    public interface IChatbotService
    {
        Task<Message?> Awnser(Message message, Room room);
    }
}
