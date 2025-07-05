using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace RenewXControl.Infrastructure.Hubs
{
    [Authorize]
    public class AssetsHub:Hub
    {
        public override Task OnConnectedAsync()
        {
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Console.WriteLine($"User connected: {userId}");

            return base.OnConnectedAsync();
        }
    }
}
