using Microsoft.AspNetCore.SignalR.Client;
using RenewXControl.Api.DTOs;
using RenewXControl.Domain.Assets;

var connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:61998/assetsHub")
    .Build();

connection.On<AssetsMonitoring>("AssetUpdate", data =>
{
    Console.Clear();
    Console.SetCursorPosition(0, 0);

    // Solar
    if (data.Solar is not null)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"[Solar] {data.Solar.AssetType}");
        Console.WriteLine($"Status Message: {data.Solar.Message}");
        Console.WriteLine($"  Sensor: {data.Solar.Irradiance}");
        Console.WriteLine($"  ActivePower: {data.Solar.ActivePower}");
        Console.WriteLine($"  SetPoint: {data.Solar.SetPoint}");
        Console.WriteLine($"  Timestamp: {data.Solar.Timestamp}");
        Console.ResetColor();
    }

    // Turbine
    if (data.Turbine is not null)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[Turbine] {data.Turbine.AssetType}");
        Console.WriteLine($"Status Message: {data.Turbine.Message}");
        Console.WriteLine($"  Sensor: {data.Turbine.WindSpeed}");
        Console.WriteLine($"  ActivePower: {data.Turbine.ActivePower}");
        Console.WriteLine($"  SetPoint: {data.Turbine.SetPoint}");
        Console.WriteLine($"  Timestamp: {data.Turbine.Timestamp}");
        Console.ResetColor();
    }

    // Battery
    if (data.Battery is not null)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"[Battery] {data.Battery.AssetType}");
        Console.WriteLine($"Status Message: {data.Battery.Message}");
        Console.WriteLine($"  Capacity: {data.Battery.Capacity}");
        Console.WriteLine($"  StateCharge: {data.Battery.StateCharge}");
        Console.WriteLine($"  RateDischarge: {data.Battery.RateDischarge}");
        Console.WriteLine($"  SetPoint: {data.Battery.SetPoint}");
        Console.WriteLine($"  Timestamp: {data.Battery.Timestamp}");
        Console.ResetColor();
    }
});

await connection.StartAsync();
Console.WriteLine("Connected. Listening for asset updates...");
Console.ReadLine();