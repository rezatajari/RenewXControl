using Microsoft.AspNetCore.SignalR;
using RenewXControl.Api.DTOs;
using RenewXControl.Api.Hubs;

namespace RenewXControl.Application.Services
{
    public class AssetsDataBroadcastService:BackgroundService
    {
        private readonly IHubContext<AssetsHub> _hubContext;

        public AssetsDataBroadcastService(IHubContext<AssetsHub> hubContext)
        {
            _hubContext= hubContext;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var random = new Random();
            while (!stoppingToken.IsCancellationRequested)
            {
                var solarDto = new SolarDto { ActivePower = random.NextDouble() * 1000 };

                await _hubContext.Clients.All.SendAsync("ReceiveSolarData", solarDto, cancellationToken: stoppingToken);

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
