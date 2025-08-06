using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RXC.Client;
using RXC.Client.Services;
using System.Net.Http.Json;
using Client;
using RXC.Client.Utility;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

// Step 1: Load appsettings manually
var httpClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
var config = await httpClient.GetFromJsonAsync<Dictionary<string, string>>($"appsettings.Development.json");

// Step 2: Read config values
var apiBaseUrl = config["ApiBaseUrl"];
var signalRHubUrl = config["SignalRHubUrl"];

// Step 3: Inject HttpClient with base address
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(apiBaseUrl)
});

// Optional: register config values in DI
builder.Services.AddSingleton(new HubConfig
{
    HubUrl = signalRHubUrl
});

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<DashboardState>();
builder.Services.AddAuthorizationCore();
await builder.Build().RunAsync();
