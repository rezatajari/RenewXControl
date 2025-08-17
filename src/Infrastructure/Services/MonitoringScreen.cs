using Application.DTOs;
using Application.DTOs.AssetMonitoring;
using Application.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Services;

public class MonitoringScreen(
    IHubContext<AssetsHub> hubContext,
    IServiceScopeFactory scopeFactory,                // ✅ Inject scope factory instead of scoped service
    List<UserMonitoringInfo> inMemoryData)            // ✅ Shared in-memory data
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Optional: Load fresh data at startup
        await RefreshFromDatabase();

        while (!stoppingToken.IsCancellationRequested)
        {
            var siteTasks = inMemoryData.Select(info => Task.Run(async () =>
            {
                // 1. Simulate asset updates
                info.SolarPanel?.Start();
                info.WindTurbine?.Start();
                info.SolarPanel?.UpdateIrradiance();
                info.WindTurbine?.UpdateWindSpeed();

                if (info is { Battery: not null, SolarPanel: not null, WindTurbine: not null })
                {
                    using var scope = scopeFactory.CreateScope();
                    var monitoringService = scope.ServiceProvider.GetRequiredService<IMonitoringService>();
                    monitoringService.ChargeDischarge(info.SolarPanel, info.WindTurbine, info.Battery);
                }

                // 2. Map to DTO
                var solarDto = new Solar(
                    "Solar Panel",
                    info.SolarPanel?.Irradiance ?? 0,
                    info.SolarPanel?.ActivePower ?? 0,
                    info.SolarPanel?.StatusMessage,
                    info.SolarPanel?.SetPoint ?? 0,
                    DateTime.UtcNow
                );

                var turbineDto = new Turbine(
                    "Wind Turbine",
                    info.WindTurbine?.WindSpeed ?? 0,
                    info.WindTurbine?.ActivePower ?? 0,
                    info.WindTurbine?.StatusMessage,
                    info.WindTurbine?.SetPoint ?? 0,
                    DateTime.UtcNow
                );

                var batteryDto = new BatteryDto(
                    "Battery",
                    info.Battery?.ChargeStateMessage,
                    info.Battery?.Capacity ?? 0,
                    info.Battery?.SetPoint ?? 0,
                    info.Battery?.StateCharge ?? 0,
                    info.Battery?.FrequentlyDisCharge ?? 0,
                    DateTime.UtcNow
                );

                var model = new UserMonitoringInfoDto(
                    info.Username,
                    info.SiteName,
                    info.SiteLocation,
                    solarDto,
                    turbineDto,
                    batteryDto
                );

                // 3. Send update immediately for this site
                await hubContext.Clients.All.SendAsync("UpdateAsset", model, stoppingToken);

            }, stoppingToken));

            // Run all site tasks in parallel (don’t block each other)
            await Task.WhenAll(siteTasks);

            await Task.Delay(1000, stoppingToken);
        }
    }

    // ✅ Safe database refresh
    public async Task RefreshFromDatabase()
    {
        using var scope = scopeFactory.CreateScope();
        var monitoringService = scope.ServiceProvider.GetRequiredService<IMonitoringService>();

        var monitoringInfo = await monitoringService.GetAllUsersWithSitesAndAssets();
        if (monitoringInfo.IsSuccess)
        {
            inMemoryData.Clear();
            inMemoryData.AddRange(monitoringInfo.Data);
        }
    }
}
