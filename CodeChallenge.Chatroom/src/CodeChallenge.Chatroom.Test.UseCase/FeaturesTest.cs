using CodeChallenge.Chatroom.Core;
using Xunit;

namespace CodeChallenge.Chatroom.Test.UseCase
{
    public class FeaturesTest
    {
        private readonly RoomCreatingService roomCreatingService;
        private readonly RoomJoiningService roomJoiningService;
        private readonly RoomLeavingService roomLeavingService;
        private readonly MessageSendingService messageSendingService;
        private readonly IRoomRepository roomRepository;
        private readonly IRoomQueryService roomQueryService;

        public FeaturesTest(
            RoomCreatingService roomCreatingService,
            RoomJoiningService roomJoiningService,
            RoomLeavingService roomLeavingService,
            MessageSendingService messageSendingService,
            IRoomRepository roomRepository,
            IRoomQueryService roomQueryService)
        {
            this.roomCreatingService = roomCreatingService;
            this.roomJoiningService = roomJoiningService;
            this.roomLeavingService = roomLeavingService;
            this.messageSendingService = messageSendingService;
            this.roomRepository = roomRepository;
            this.roomQueryService = roomQueryService;
        }

        [Fact]
        public async Task CreateRoom()
        {
            var exception = await Record.ExceptionAsync(async () => await roomCreatingService.Create("room name"));

            Assert.Null(exception);
        }

        [Fact]
        public async Task GetRoom()
        {
            var roomId = await roomCreatingService.Create("room name");
            var room = await roomQueryService.Get(roomId);

            Assert.Equal(roomId, room?.Id);
        }

        [Fact]
        public async Task SendMessage()
        {
            var roomId = await roomCreatingService.Create("room name");
            var userId = Guid.NewGuid();

            await roomJoiningService.Join(roomId, userId);
            await messageSendingService.Send("test message", userId);

            var room = await roomQueryService.Get(roomId);

            Assert.Contains("test message", room?.Messages.Select(m => m.Content));
        }

        [Fact]
        public async Task QuoteMessage()
        {
            var roomId = await roomCreatingService.Create("room name");
            var userId = Guid.NewGuid();

            await roomJoiningService.Join(roomId, userId);
            await messageSendingService.Send("test message", userId);

            var room = await roomQueryService.Get(roomId);
            var firstMessage = room!.Messages.First();

            await messageSendingService.Quote(firstMessage.Id, "test quote", userId);

            room = await roomQueryService.Get(roomId);

            var quote = room!.Messages.FirstOrDefault(m => m.Content == "test quote");

            Assert.NotNull(quote);
            Assert.Equal(firstMessage.Id, quote?.QuoteeMessageId);
        }

        [Fact]
        public async Task GetAllRooms()
        {
            var roomId = await roomCreatingService.Create("room name");
            var anotherRoomId = await roomCreatingService.Create("room name");

            var rooms = await roomQueryService.GetSummaries();

            Assert.Contains(roomId, rooms.Select(r => r.Id));
            Assert.Contains(anotherRoomId, rooms.Select(r => r.Id));
        }

        [Fact]
        public async Task JoinRoom()
        {
            var roomId = await roomCreatingService.Create("room name");
            var userId = Guid.NewGuid();

            await roomJoiningService.Join(roomId, userId);

            var joinedRoom = await roomRepository.GetOfUserId(userId);

            Assert.Contains(userId, joinedRoom?.MemberIds);
        }

        [Fact]
        public async Task LeaveRoom()
        {
            var roomId = await roomCreatingService.Create("room name");
            var userId = Guid.NewGuid();

            await roomJoiningService.Join(roomId, userId);
            await roomLeavingService.Leave(userId);

            var joinedRoom = await roomRepository.GetOfUserId(userId);

            Assert.Null(joinedRoom);
        }
    }
}