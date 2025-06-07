using Microsoft.AspNetCore.SignalR.Client;
using RenewXControl.Api.DTOs;

var connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:61998/assetsHub") // Use your actual URL and port
    .Build();

connection.On<SolarDto>("ReceiveSolarData", data =>
{
    Console.WriteLine($"Received Solar Data: ActivePower={data.ActivePower}");
});

await connection.StartAsync();
Console.WriteLine("Connected. Listening for data...");
Console.ReadLine();