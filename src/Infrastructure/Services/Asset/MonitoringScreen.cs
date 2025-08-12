using Application.DTOs.AssetMonitoring;
using Application.Implementations.Monitoring;
using Application.Interfaces.Monitoring;
using Domain.Entities.User;
using Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

namespace Infrastructure.Services.Asset;

public class MonitoringScreen(
    ConnectedUsersStore store,
    IHubContext<AssetsHub> hubContext,
    IMonitoringService monitoringService)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var users = store.GetAll();

            foreach (var userId in users)
            {
                var result = await monitoringService.MonitoringAssetControl(userId);

                if (!result.IsSuccess) continue;

                // Start asset simulation if not already running
                result.Data.SolarControl?.Start();
                result.Data.TurbineControl?.Start();

                // Update simulation values
                result.Data.SolarControl?.UpdateIrradiance();
                result.Data.TurbineControl?.UpdateWindSpeed();
                _ = Task.Run(() => result.Data.AssetOperations?.ChargeDischarge());

                // Build DTOs
                var solarDto = new Solar(
                    AssetType: "Solar Panel",
                    Message: result.Data.SolarControl?.StatusMessage,
                    Irradiance: result.Data.SolarControl?.Irradiance ?? 0,
                    ActivePower: result.Data.SolarControl?.ActivePower ?? 0,
                    SetPoint: result.Data.SolarControl?.SetPoint ?? 0,
                    Timestamp: DateTime.UtcNow
                );

                var turbineDto = new Turbine(
                    AssetType: "Wind Turbine",
                    Message: result.Data.TurbineControl?.StatusMessage,
                    WindSpeed: result.Data.TurbineControl?.WindSpeed ?? 0,
                    ActivePower: result.Data.TurbineControl?.ActivePower ?? 0,
                    SetPoint: result.Data.TurbineControl?.SetPoint ?? 0,
                    Timestamp: DateTime.UtcNow
                );

                var batteryDto = new BatteryDto(
                    AssetType: "Battery",
                    Message: result.Data.BatteryControl?.ChargeStateMessage,
                    Capacity: result.Data.BatteryControl?.Capacity ?? 0,
                    SetPoint: result.Data.BatteryControl?.SetPoint ?? 0,
                    StateCharge: result.Data.BatteryControl?.StateCharge ?? 0,
                    RateDischarge: result.Data.BatteryControl?.FrequentlyDisCharge ?? 0,
                    Timestamp: DateTime.UtcNow
                );

                var model = new AssetsMonitoring(solarDto, turbineDto, batteryDto);

                // Send only to that user's group
                await hubContext.Clients.Group(userId.ToString())
                    .SendAsync(method: "AssetUpdate", model, stoppingToken);
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}