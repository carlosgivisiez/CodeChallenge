namespace CodeChallenge.Chatroom.Core
{
    public class RoomJoiningService
    {
        private readonly IRoomRepository roomRepository;

        public RoomJoiningService(IRoomRepository roomRepository)
        {
            this.roomRepository = roomRepository;
        }

        public async Task Join(Guid roomId, Guid userId)
        {
            var currentRoom = await roomRepository.GetOfUserId(userId);

            if (currentRoom != null)
            {
                currentRoom.RemoveMember(userId);

                await roomRepository.Put(currentRoom);
            }

            var newRoom = await roomRepository.Get(roomId);

            if (newRoom == null)
            {
                throw new ArgumentException("Room not found");
            }

            newRoom.AddMember(userId);

            await roomRepository.Put(newRoom);
        }
    }
}
