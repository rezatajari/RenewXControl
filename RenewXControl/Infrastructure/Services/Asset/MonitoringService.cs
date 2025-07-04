﻿using Microsoft.AspNetCore.SignalR;
using RenewXControl.Application.Asset.Interfaces;
using RenewXControl.Application.DTOs.AssetMonitoring;
using RenewXControl.Domain.Interfaces.Assets;
using RenewXControl.Infrastructure.Hubs;

namespace RenewXControl.Infrastructure.Services.Asset
{
    public class MonitoringService : BackgroundService
    {
        private readonly IHubContext<AssetsHub> _hub;
        private readonly ISolarControl _solarControl;
        private readonly ITurbineControl _turbineControl;
        private readonly IBatteryControl _batteryControl;
        private readonly IAssetRuntimeOperation _assetRuntimeOperation;


        public MonitoringService(
            ISolarControl solarControl,
            ITurbineControl turbineControl,
            IBatteryControl batteryControl,
            IAssetRuntimeOperation assetRuntimeOperation,
            IHubContext<AssetsHub> hubContext)
        {
            _solarControl = solarControl;
            _turbineControl= turbineControl;
            _batteryControl = batteryControl;
            _assetRuntimeOperation = assetRuntimeOperation;
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
                Task.Run(async  ()=> _assetRuntimeOperation.ChargeDischarge());
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
