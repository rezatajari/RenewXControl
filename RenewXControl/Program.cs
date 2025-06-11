using RenewXControl.Configuration.AssetsModel.Assets;
using RenewXControl.Domain.Assets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RenewXControl.Api.Hubs;
using RenewXControl.Application.Services;
using RenewXControl.Infrastructure.Persistence.MyDbContext;
using Battery = RenewXControl.Domain.Assets.Battery;
using RenewXControl.Application.Services.Assets;
using RenewXControl.Domain.Interfaces.Assets;
using RenewXControl.Domain.Implementatons.Assets;
using RenewXControl.Application.Interfaces.Assets;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddSingleton<SolarPanel>(sp =>
{
    var config=sp.GetRequiredService<IOptions<SolarPanelConfig>>().Value;
    return SolarPanel.Create(config);
});
builder.Services.AddSingleton<WindTurbine>(wt =>
{
    var config = wt.GetRequiredService<IOptions<WindTurbineConfig>>().Value;
    return WindTurbine.Create(config);
});
builder.Services.AddSingleton<Battery>(b =>
{
    var config = b.GetRequiredService<IOptions<BatteryConfig>>().Value;
    return Battery.Create(config);
});

// ✅ 4. Register solar & turbine services and interfaces
builder.Services.AddSingleton<ISolarControl, SolarControl>();
builder.Services.AddSingleton<ISolarService, SolarService>();
builder.Services.AddSingleton<ITurbineService, TurbineService>();
builder.Services.AddSingleton<ITurbineControl, TurbineControl>();
builder.Services.AddSingleton<IBatteryControl, BatteryControl>();
builder.Services.AddSingleton<IBatteryService, BatteryService>();
builder.Services.AddSingleton<IAssetService, AssetService>();

// ✅ 5. Others
builder.Services.AddDbContext<RxcDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSignalR();
builder.Services.AddHostedService<MonitoringService>();
builder.Services.AddControllers();

var app = builder.Build();
app.MapHub<AssetsHub>("/assetsHub");
app.MapControllers();
app.Run();

// var userConfig = builder.Configuration.GetSection("UserConfig").Get<UserConfig>();
// var siteConfig = builder.Configuration.GetSection("SiteConfig").Get<SiteConfig>();

// Factory pattern to create each our models
// var user = User.Create(userConfig.Name);
// var site = Site.Create(siteConfig);

// Add assets to the site
// site.AddAsset(windTurbine);
// site.AddAsset(solarPanel);
// site.AddAsset(battery);

// user.AddSite(site); // Add site to the user

//var rxc = new RXCApp();
// Run the RXCApp
// await rxc.Run(user, site);




