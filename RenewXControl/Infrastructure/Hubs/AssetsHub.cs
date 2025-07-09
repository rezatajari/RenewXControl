using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace RenewXControl.Infrastructure.Hubs
{
    [Authorize]
    public class AssetsHub:Hub
    {
        public override async Task OnConnectedAsync()
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, userId);
                Console.WriteLine($"User connected and added to group: {userId}");
            }

            await base.OnConnectedAsync();
        }
    }
}
