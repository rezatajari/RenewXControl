using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http.Json;
using WebClient;
using WebClient.Services;
using WebClient.Utility;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

// Step 1: Determine config file based on environment
var environment = builder.HostEnvironment.Environment;
var configFile = $"appsettings.{environment}.json";

// Step 2: Fetch config from the correct file
var httpClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
var config = await httpClient.GetFromJsonAsync<AppSettings>(configFile);

if (config is null)
    throw new Exception($"Failed to load configuration from {configFile}");

// Step 3: Register HttpClient using the ApiBaseUrl
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(config.ApiBaseUrl)
});

// Step 4: Inject SignalR Hub config
builder.Services.AddSingleton(new HubConfig
{
    HubUrl = config.SignalRHubUrl
});
Console.WriteLine($"Loaded ApiBaseUrl: {config.ApiBaseUrl}");
Console.WriteLine($"Loaded SignalRHubUrl: {config.SignalRHubUrl}");

// Step 5: Register app services
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<DashboardState>();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();