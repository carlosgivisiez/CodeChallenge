namespace CodeChallenge.Chatroom.Core
{
    public interface IRoomQueryService
    {
        Task<IEnumerable<RoomSummary>> GetSummaries();
        Task<Room?> Get(Guid id);
    }
}
