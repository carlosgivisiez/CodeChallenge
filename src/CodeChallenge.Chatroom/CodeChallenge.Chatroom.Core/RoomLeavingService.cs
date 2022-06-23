namespace CodeChallenge.Chatroom.Core
{
    public class RoomLeavingService
    {
        private readonly IRoomRepository roomRepository;

        public RoomLeavingService(IRoomRepository roomRepository)
        {
            this.roomRepository = roomRepository;
        }

        public async Task Leave(Guid userId)
        {
            var room = await roomRepository.GetOfUserId(userId);

            if (room == null)
            {
                throw new ArgumentException("Joined room not found");
            }

            room.RemoveMember(userId);

            await roomRepository.Put(room);
        }
    }
}
