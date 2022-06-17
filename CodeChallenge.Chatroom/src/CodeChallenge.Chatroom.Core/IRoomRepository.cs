namespace CodeChallenge.Chatroom.Core
{
    public interface IRoomRepository
    {
        Task<Room?> Get(Guid id);
        Task<Room?> GetOfUserId(Guid userId);
        Task<Room?> GetOfMessageId(Guid messageId, Guid userId);
        Task Put(Room room);
        Task Delete(Room room);
    }
}
