using Microsoft.AspNetCore.Identity;
using RenewXControl.Configuration.AssetsModel.Assets;
using RenewXControl.Domain.Assets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.ResponseCompression;
using Battery = RenewXControl.Domain.Assets.Battery;
using RenewXControl.Domain.Interfaces.Assets;
using RenewXControl.Domain.Implementatons.Assets;
using RenewXControl.Domain.Users;
using RenewXControl.Infrastructure.Services;
using RenewXControl.Infrastructure.Persistence;
using RenewXControl.Infrastructure.Hubs;
using RenewXControl.Application.User;
using RenewXControl.Application.Asset.Interfaces;
using RenewXControl.Application.Asset.Implementation;
using RenewXControl.Application.Common;
using RenewXControl.Infrastructure.Services.User;
using RenewXControl.Infrastructure.Services.Asset;

var builder = WebApplication.CreateBuilder(args);

// 👇 Add this CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy.WithOrigins("http://localhost:5187") // URL of your Blazor client
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); // Needed for SignalR
    });
});

// ✅ 1. load config
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// ✅ 2. Bind it
builder.Services.Configure<SolarPanelConfig>(
    builder.Configuration.GetSection("SolarPanelConfig"));
builder.Services.Configure<WindTurbineConfig>(
    builder.Configuration.GetSection("WindTurbineConfig"));
builder.Services.Configure<BatteryConfig>(
    builder.Configuration.GetSection("BatteryConfig"));

// ✅ 3. SolarPanel & Turbine setup
//builder.Services.AddSingleton<SolarPanel>(sp =>
//{
//    var config=sp.GetRequiredService<IOptions<SolarPanelConfig>>().Value;
//    return SolarPanel.Create(config);
//});
//builder.Services.AddSingleton<WindTurbine>(wt =>
//{
//    var config = wt.GetRequiredService<IOptions<WindTurbineConfig>>().Value;
//    return WindTurbine.Create(config);
//});
//builder.Services.AddSingleton<Battery>(b =>
//{
//    var config = b.GetRequiredService<IOptions<BatteryConfig>>().Value;
//    return Battery.Create(config);
//});

// ✅ 4. Register solar & turbine services and interfaces
builder.Services.AddSingleton<ISolarControl, SolarControl>();
//builder.Services.AddSingleton<ISolarService, SolarService>();
//builder.Services.AddSingleton<ITurbineService, TurbineService>();
builder.Services.AddSingleton<ITurbineControl, TurbineControl>();
builder.Services.AddSingleton<IBatteryControl, BatteryControl>();
//builder.Services.AddSingleton<IBatteryService, BatteryService>();
builder.Services.AddSingleton<IAssetService, AssetService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAssetRepository, AssetRepository>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<ISiteRepository, SiteRepository>();

// ✅ 5. Others
builder.Services.AddDbContext<RxcDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<RxcDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddSignalR();
builder.Services.AddHostedService<MonitoringService>();
builder.Services.AddControllers();

var app = builder.Build();

// 👇 Use the CORS policy before endpoints
app.UseCors("AllowBlazorClient");

app.UseAuthentication();
app.UseAuthorization();
app.MapHub<AssetsHub>("/assetsHub").RequireAuthorization();
app.MapControllers();
app.Run();




