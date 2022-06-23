namespace CodeChallenge.Chatroom.Core
{
    public class RoomCreatingService
    {
        private readonly IRoomRepository roomRepository;

        public RoomCreatingService(IRoomRepository roomRepository)
        {
            this.roomRepository = roomRepository;
        }

        public async Task<Guid> Create(string name)
        {
            var room = new Room(name);

            await roomRepository.Put(room);

            return room.Id;
        }
    }
}
