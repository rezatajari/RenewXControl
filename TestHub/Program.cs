using Microsoft.AspNetCore.SignalR.Client;
using RenewXControl.Api.DTOs;

var connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:61998/assetsHub") // Use your actual URL and port
    .Build();

connection.On<MonitoringDto>("AssetUpdate", data =>
{
    switch (data.AssetType)
    {
        case "SolarPanel":
            Console.ForegroundColor = ConsoleColor.Yellow;
            break;
        case "WindTurbine":
            Console.ForegroundColor = ConsoleColor.Red;
            break;
        case "Battery":
            Console.ForegroundColor = ConsoleColor.Cyan;
            break;
        default:
            Console.ResetColor();
            break;
    }

    Console.Write($"[{data.Timestamp:HH:mm:ss}] {data.AssetType} ({data.AssetName}) | ");
    Console.Write($"Message: {data.Message} Sensor: {data.SensorValue} | ActivePower: {data.ActivePower} | SetPoint: {data.SetPoint}");

    if (data.AssetType == "Battery")
    {
        Console.Write($" | StateCharge: {data.StateCharge} | RateDischarge: {data.RateDischarge}");
    }

    Console.WriteLine();
    Console.ResetColor();
});

await connection.StartAsync();
Console.WriteLine("Connected. Listening for asset updates...");
Console.ReadLine();