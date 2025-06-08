using Microsoft.AspNetCore.SignalR;
using RenewXControl.Api.DTOs;
using RenewXControl.Api.Hubs;
using RenewXControl.Application.Interfaces;

namespace RenewXControl.Application.Services
{
    public class MonitoringService : BackgroundService
    {
        private readonly IHubContext<AssetsHub> _hub;
        private readonly ISolarService _solarService;
        private readonly ITurbineService _turbineService;


        public MonitoringService(
            ISolarService solarService,
            ITurbineService turbineService,
            IHubContext<AssetsHub> hubContext)
        {
            _solarService = solarService;
            _turbineService = turbineService;
            _hub = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Update and get solar panel data
                _solarService.UpdateIrradiance();
                _solarService.UpdateActivePower();

                var solarDto = new MonitoringDto
                {
                    AssetType = "SolarPanel",
                    AssetName = "Main Solar", // Replace with actual name if available
                    SensorValue = _solarService.GetIrradiance(),
                    ActivePower = _solarService.GetActivePower(),
                    SetPoint = 0, // Set actual set point if available
                    Timestamp = DateTime.UtcNow
                };

                // Update and get wind turbine data
                _turbineService.UpdateWindSpeed();
                _turbineService.UpdateActivePower();
                var turbineDto = new MonitoringDto
                {
                    AssetType = "WindTurbine",
                    AssetName = "Main Turbine", // Replace with actual name if available
                    SensorValue = _turbineService.GetWindSpeed(),
                    ActivePower = _turbineService.GetActivePower(),
                    SetPoint = 0, // Set actual set point if available
                    Timestamp = DateTime.UtcNow
                };

                // Send both DTOs to all clients
                await _hub.Clients.All.SendAsync("AssetUpdate", solarDto, stoppingToken);
                await _hub.Clients.All.SendAsync("AssetUpdate", turbineDto, stoppingToken);

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
