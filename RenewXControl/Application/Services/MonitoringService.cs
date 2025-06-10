using Microsoft.AspNetCore.SignalR;
using RenewXControl.Api.DTOs;
using RenewXControl.Api.Hubs;
using RenewXControl.Application.Interfaces.Assets;

namespace RenewXControl.Application.Services
{
    public class MonitoringService : BackgroundService
    {
        private readonly IHubContext<AssetsHub> _hub;
        private readonly ISolarService _solarService;
        private readonly ITurbineService _turbineService;
        private readonly IBatteryService _batteryService;
        private readonly IAssetControl _assetControl;


        public MonitoringService(
            ISolarService solarService,
            ITurbineService turbineService,
            IBatteryService batteryService,
            IAssetControl assetControl,
            IHubContext<AssetsHub> hubContext)
        {
            _solarService = solarService;
            _turbineService = turbineService;
            _batteryService = batteryService;
            _assetControl = assetControl;
            _hub = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var startSolar = _solarService.StartGenerator();
            var startTurbine=_solarService.StartGenerator();

            while (!stoppingToken.IsCancellationRequested)
            {
                _solarService.UpdateIrradiance();
                _solarService.UpdateActivePower();

                var solarDto = new MonitoringDto
                {
                    AssetType = "SolarPanel",
                    AssetName = "Main Solar",
                    Message = startSolar.Message,
                    SensorValue = _solarService.GetIrradiance,
                    ActivePower = _solarService.GetActivePower,
                    SetPoint = 0,
                    StateCharge = null,
                    RateDischarge = null,
                    Timestamp = DateTime.UtcNow
                };
                // Turbine
                _turbineService.UpdateWindSpeed();
                _turbineService.UpdateActivePower();
                var turbineDto = new MonitoringDto
                {
                    AssetType = "WindTurbine",
                    AssetName = "Main Turbine",
                    Message = startTurbine.Message,
                    SensorValue = _turbineService.GetWindSpeed,
                    ActivePower = _turbineService.GetActivePower,
                    SetPoint = 0,
                    StateCharge = null,
                    RateDischarge = null,
                    Timestamp = DateTime.UtcNow
                };

                // Battery
                await _assetControl.ChargeDischarge();
                var batteryDto = new MonitoringDto
                {
                    AssetType = "Battery",
                    AssetName = "Main Battery",
                    SetPoint = _batteryService.GetSetPoint, // set if available
                    StateCharge = _batteryService.GetStateCharge,
                    RateDischarge = _batteryService.GetFrequentlyDisCharge,
                    Timestamp = DateTime.UtcNow
                };

                // Send both DTOs to all clients
                await _hub.Clients.All.SendAsync("AssetUpdate", solarDto, stoppingToken);
                await _hub.Clients.All.SendAsync("AssetUpdate", turbineDto, stoppingToken);
                await _hub.Clients.All.SendAsync("AssetUpdate", batteryDto, stoppingToken);

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
