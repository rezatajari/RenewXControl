using System.Security.Claims;
using Application.Implementations.Monitoring;
using Application.Interfaces.Monitoring;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Hubs;

[Authorize]
public class AssetsHub:Hub
{
    private readonly ConnectedUsersStore _store;

    public AssetsHub(ConnectedUsersStore store)
    {
        _store = store;
    }
    public override async Task OnConnectedAsync()
    {
        var userIdStr = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (Guid.TryParse(userIdStr, out var userId))
        {
            _store.Add(userId);
            await Groups.AddToGroupAsync(Context.ConnectionId, userId.ToString());
        }
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userIdStr = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (Guid.TryParse(userIdStr, out var userId))
        {
            _store.Remove(userId);
        }
        await base.OnDisconnectedAsync(exception);
    }
}