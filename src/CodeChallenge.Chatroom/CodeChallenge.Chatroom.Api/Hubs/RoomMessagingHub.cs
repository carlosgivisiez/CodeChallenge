using CodeChallenge.Chatroom.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace CodeChallenge.Chatroom.Api.Hubs
{
    [Authorize]
    public class RoomMessagingHub : Hub
    {
        private readonly IRoomQueryService roomQueryService;
        private readonly IRoomRepository roomRepository;
        private readonly RoomCreatingService roomCreatingService;
        private readonly RoomJoiningService roomJoiningService;
        private readonly RoomLeavingService roomLeavingService;
        private readonly MessageSendingService messageSendingService;

        public RoomMessagingHub(
            IRoomQueryService roomQueryService,
            IRoomRepository roomRepository,
            RoomCreatingService roomCreatingService,
            RoomJoiningService roomJoiningService,
            RoomLeavingService roomLeavingService,
            MessageSendingService messageSendingService)
        {
            this.roomQueryService = roomQueryService;
            this.roomRepository = roomRepository;
            this.roomCreatingService = roomCreatingService;
            this.roomJoiningService = roomJoiningService;
            this.roomLeavingService = roomLeavingService;
            this.messageSendingService = messageSendingService;
        }

        public async Task<IEnumerable<RoomSummary>> GetRooms()
        {
            return await roomQueryService.GetSummaries();
        }

        public async Task<Room?> GetRoom(Guid roomId)
        {
            return await roomQueryService.Get(roomId);
        }

        public async Task CreateRoom(string name)
        {
            await roomCreatingService.Create(name);

            await Clients.All.SendAsync("roomsUpdated");
        }

        public async Task JoinRoom(Guid id)
        {
            var userId = Guid.Parse(Context.UserIdentifier!);

            await roomJoiningService.Join(id, userId);
            await Groups.AddToGroupAsync(Context.ConnectionId, id.ToString());

            await Clients.All.SendAsync("roomsUpdated");
        }

        public async Task LeaveRoom(Guid id)
        {
            var userId = Guid.Parse(Context.UserIdentifier!);

            await roomLeavingService.Leave(userId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, id.ToString());

            await Clients.All.SendAsync("roomsUpdated");
        }

        public async Task SendMessage(string content, Guid roomId)
        {
            var userId = Guid.Parse(Context.UserIdentifier!);

            var actualRoom = await roomRepository.GetOfUserId(userId);

            if (actualRoom?.Id != roomId)
            {
                return;
            }

            await messageSendingService.Send(content, userId);

            await Clients.Group(roomId.ToString()).SendAsync("messagesUpdated");
        }
    }
}
