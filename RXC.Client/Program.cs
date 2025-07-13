using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using RXC.Client;
using RXC.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:61998") // 👈 backend API port
});

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<DashboardState>();
builder.Services.AddAuthorizationCore();
await builder.Build().RunAsync();
