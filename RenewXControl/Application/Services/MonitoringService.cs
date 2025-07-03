using Microsoft.AspNetCore.SignalR;
using RenewXControl.Api.DTOs;
using RenewXControl.Api.Hubs;
using RenewXControl.Application.Interfaces.Assets;
using RenewXControl.Domain.Interfaces.Assets;

namespace RenewXControl.Application.Services
{
    public class MonitoringService : BackgroundService
    {
        private readonly IHubContext<AssetsHub> _hub;
        private readonly ISolarControl _solarControl;
        private readonly ITurbineControl _turbineControl;
        private readonly IBatteryControl _batteryControl;
        private readonly IAssetService _assetService;


        public MonitoringService(
            ISolarControl solarControl,
            ITurbineControl turbineControl,
            IBatteryControl batteryControl,
            IAssetService assetService,
            IHubContext<AssetsHub> hubContext)
        {
            _solarControl = solarControl;
            _turbineControl= turbineControl;
            _batteryControl = batteryControl;
            _assetService = assetService;
            _hub = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _solarControl.Start();
            _turbineControl.Start();

            while (!stoppingToken.IsCancellationRequested)
            {

                // Solar
                _solarControl.UpdateIrradiance();
                var solarDto = new Solar
                (
                   AssetType: "SolarPanel",
                   Message: _solarControl.StatusMessage,
                   Irradiance: _solarControl.Irradiance,
                   ActivePower: _solarControl.ActivePower,
                   SetPoint: _solarControl.SetPoint,
                   Timestamp: DateTime.UtcNow
                );

                // Turbine
                _turbineControl.UpdateWindSpeed();
                var turbineDto = new Turbine
                (
                    AssetType: "WindTurbine",
                    Message: _turbineControl.StatusMessage,
                    WindSpeed: _turbineControl.WindSpeed,
                    ActivePower: _turbineControl.ActivePower,
                    SetPoint: _turbineControl.SetPoint,
                    Timestamp: DateTime.UtcNow
                );

                // Battery
                Task.Run(async  ()=>_assetService.ChargeDischarge());
                var batteryDto = new Battery
                (
                    AssetType: "Battery",
                    Message: _batteryControl.ChargeStateMessage,
                    Capacity: _batteryControl.Capacity,
                    SetPoint: _batteryControl.SetPoint, // set if available
                    StateCharge: _batteryControl.StateCharge,
                    RateDischarge: _batteryControl.FrequentlyDisCharge,
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
