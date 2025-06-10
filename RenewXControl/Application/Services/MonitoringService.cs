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
            var startTurbine = _turbineService.StartGenerator();

            while (!stoppingToken.IsCancellationRequested)
            {

                // Solar
                _solarService.UpdateIrradiance();
                _solarService.UpdateActivePower();
                var solarDto = new Solar
                (
                   AssetType: "SolarPanel",
                   Message: startSolar.Message,
                   Irradiance: _solarService.GetIrradiance,
                   ActivePower: _solarService.GetActivePower,
                   SetPoint: _solarService.GetSetPoint,
                   Timestamp: DateTime.UtcNow
                );

                // Turbine
                _turbineService.UpdateWindSpeed();
                _turbineService.UpdateActivePower();
                var turbineDto = new Turbine
                (
                    AssetType: "WindTurbine",
                    Message: startTurbine.Message,
                    WindSpeed: _turbineService.GetWindSpeed,
                    ActivePower: _turbineService.GetActivePower,
                    SetPoint: _turbineService.GetSetPoint,
                    Timestamp: DateTime.UtcNow
                );

                // Battery
                await _assetControl.ChargeDischarge();
                var batteryDto = new Battery
                (
                    AssetType: "Battery",
                    Message: _batteryService.GetChargeStateMessage,
                    Capacity: _batteryService.GetCapacity,
                    SetPoint: _batteryService.GetSetPoint, // set if available
                    StateCharge: _batteryService.GetStateCharge,
                    RateDischarge: _batteryService.GetFrequentlyDisCharge,
                    Timestamp: DateTime.UtcNow
                );

                var model = new AssetsMonitoring(solarDto, turbineDto, batteryDto);

                // Send both DTOs to all clients
                await _hub.Clients.All.SendAsync("AssetUpdate", model, stoppingToken);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
