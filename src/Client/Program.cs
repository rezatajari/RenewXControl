using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RXC.Client;
using RXC.Client.Services;
using System.Net.Http.Json;
using Client;
using Client.Utility;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

// Step 1: Load appsettings manually
var configFile = builder.HostEnvironment.IsDevelopment()
    ? "appsettings.Development.json"
    : "appsettings.Production.json";

var httpClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
var config = await httpClient.GetFromJsonAsync<Dictionary<string, string>>(configFile);

var apiBaseUrl = config["ApiBaseUrl"];
var signalRHubUrl = config["SignalRHubUrl"];


// Step 3: Inject HttpClient with base address
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://alirezanuri70-001-site1.mtempurl.com")
});

// Optional: register config values in DI
builder.Services.AddSingleton(new HubConfig
{
    HubUrl = "http://alirezanuri70-001-site1.mtempurl.com/assetsHub/"
});

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<DashboardState>();
builder.Services.AddAuthorizationCore();
await builder.Build().RunAsync();
