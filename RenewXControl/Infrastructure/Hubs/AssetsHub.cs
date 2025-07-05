using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace RenewXControl.Infrastructure.Hubs
{
    [Authorize]
    public class AssetsHub:Hub
    {
        // No need to define SendSolarData here if only the server sends data.
        // Keep this class empty unless you want to allow clients to call hub methods.
    }
}
