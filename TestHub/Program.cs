using Microsoft.AspNetCore.SignalR.Client;
using RenewXControl.Api.DTOs;

var connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:61998/assetsHub") // Use your actual URL and port
    .Build();

connection.On<MonitoringDto>("AssetUpdate", data =>
{
    if (data.AssetType == "SolarPanel")
        Console.ForegroundColor = ConsoleColor.Yellow;
    else if (data.AssetType == "WindTurbine")
        Console.ForegroundColor = ConsoleColor.Red;
    else
        Console.ResetColor();

    Console.WriteLine(
        $"[{data.Timestamp:HH:mm:ss}] {data.AssetType} ({data.AssetName}) | " +
        $"Sensor: {data.SensorValue} | ActivePower: {data.ActivePower} | SetPoint: {data.SetPoint}"
    );
    Console.ResetColor();
});

await connection.StartAsync();
Console.WriteLine("Connected. Listening for asset updates...");
Console.ReadLine();