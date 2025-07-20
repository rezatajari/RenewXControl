using Microsoft.AspNetCore.SignalR;
using RenewXControl.Application.Asset.Interfaces.Monitoring;
using RenewXControl.Application.DTOs.AssetMonitoring;
using RenewXControl.Infrastructure.Hubs;

namespace RenewXControl.Infrastructure.Services.Asset
{
    public class MonitoringScreen : BackgroundService
    {
        private readonly IHubContext<AssetsHub> _hub;
        private readonly IMonitoringRegistry _registry;

        public MonitoringScreen(
            IHubContext<AssetsHub> hubContext,
            IMonitoringRegistry registry)
        {
            _hub = hubContext;
            _registry = registry;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                foreach (var session in _registry.GetAllSessions())
                {
                    // Start asset simulation if not already running
                    session.SolarControl?.Start();
                    session.TurbineControl?.Start();

                    // Update simulation values
                    session.SolarControl?.UpdateIrradiance();
                    session.TurbineControl?.UpdateWindSpeed();
                    _ = Task.Run(() => session.AssetRuntimeOperation?.ChargeDischarge());

                    // Build DTOs
                    var solarDto = new Solar(
                        AssetType: "Solar Panel",
                        Message: session.SolarControl?.StatusMessage,
                        Irradiance: session.SolarControl?.Irradiance ?? 0,
                        ActivePower: session.SolarControl?.ActivePower ?? 0,
                        SetPoint: session.SolarControl?.SetPoint ?? 0,
                        Timestamp: DateTime.UtcNow
                    );

                    var turbineDto = new Turbine(
                        AssetType: "Wind Turbine",
                        Message: session.TurbineControl?.StatusMessage,
                        WindSpeed: session.TurbineControl?.WindSpeed ?? 0,
                        ActivePower: session.TurbineControl?.ActivePower ?? 0,
                        SetPoint: session.TurbineControl?.SetPoint ?? 0,
                        Timestamp: DateTime.UtcNow
                    );

                    var batteryDto = new BatteryDto(
                        AssetType: "Battery",
                        Message: session.BatteryControl?.ChargeStateMessage,
                        Capacity: session.BatteryControl?.Capacity ?? 0,
                        SetPoint: session.BatteryControl?.SetPoint ?? 0,
                        StateCharge: session.BatteryControl?.StateCharge ?? 0,
                        RateDischarge: session.BatteryControl?.FrequentlyDisCharge ?? 0,
                        Timestamp: DateTime.UtcNow
                    );

                    var model = new AssetsMonitoring(solarDto, turbineDto, batteryDto);

                    // Send only to that user's group
                    await _hub.Clients.Group(session.UserId).SendAsync(method:"AssetUpdate", model, stoppingToken);
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
