using RenewXControl.Configuration.AssetsModel.Assets;
using RenewXControl.Domain.Assets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RenewXControl.Api.Hubs;
using RenewXControl.Application.Interfaces;
using RenewXControl.Application.Services;
using RenewXControl.Domain.Assets.Implementatons;
using RenewXControl.Domain.Assets.Interfaces;
using RenewXControl.Infrastructure.Persistence.MyDbContext;

var builder = WebApplication.CreateBuilder(args);

// ✅ 1. load config
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// ✅ 2. Bind it
builder.Services.Configure<SolarPanelConfig>(
    builder.Configuration.GetSection("SolarPanelConfig"));
builder.Services.Configure<WindTurbineConfig>(
    builder.Configuration.GetSection("WindTurbineConfig"));

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

// ✅ 4. Register solar & turbine services and interfaces
builder.Services.AddSingleton<ISolarActive, SolarActive>();
builder.Services.AddSingleton<ISolarService, SolarService>();
builder.Services.AddSingleton<ITurbineService, TurbineService>();
builder.Services.AddSingleton<ITurbineActive, TurbineActive>();

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

// Bind and register configuration
// var batteryConfig = builder.Configuration.GetSection("BatteryConfig").Get<BatteryConfig>();
// var userConfig = builder.Configuration.GetSection("UserConfig").Get<UserConfig>();
// var siteConfig = builder.Configuration.GetSection("SiteConfig").Get<SiteConfig>();


// Factory pattern to create each our models
// var user = User.Create(userConfig.Name);
// var site = Site.Create(siteConfig);
// var battery = Battery.Create(batteryConfig);

// Add assets to the site
// site.AddAsset(windTurbine);
// site.AddAsset(solarPanel);
// site.AddAsset(battery);

// user.AddSite(site); // Add site to the user

//var rxc = new RXCApp();
// Run the RXCApp
// await rxc.Run(user, site);




