using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace CodeChallenge.Chatroom.Api.Hubs
{
    public class UserIdProvider : IUserIdProvider
    {
        public virtual string? GetUserId(HubConnectionContext connection)
        {
            return connection.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
