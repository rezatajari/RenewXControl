using RenewXControl;
using RenewXControl.Configuration.AssetsModel.Assets;
using RenewXControl.Configuration.AssetsModel.Users;
using RenewXControl.Domain.Assets;
using RenewXControl.Domain.Users;
using Microsoft.EntityFrameworkCore;
using RenewXControl.Api.Hubs;
using RenewXControl.Application.Services;
using RenewXControl.Infrastructure.Persistence.MyDbContext;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RxcDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSignalR();
builder.Services.AddHostedService<AssetsDataBroadcastService>();
builder.Services.AddControllers();

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var app = builder.Build();
app.MapHub<AssetsHub>("/assetsHub");
app.MapControllers();
app.Run();


// Bind and register configuration
var batteryConfig = builder.Configuration.GetSection("BatteryConfig").Get<BatteryConfig>();
var solarPanelConfig = builder.Configuration.GetSection("SolarPanelConfig").Get<SolarPanelConfig>();
var windTurbineConfig = builder.Configuration.GetSection("WindTurbineConfig").Get<WindTurbineConfig>();
var userConfig = builder.Configuration.GetSection("UserConfig").Get<UserConfig>();
var siteConfig = builder.Configuration.GetSection("SiteConfig").Get<SiteConfig>();


// Factory pattern to create each our models
var user = User.Create(userConfig.Name);
var site = Site.Create(siteConfig);
var windTurbine = WindTurbine.Create(windTurbineConfig);
var solarPanel = SolarPanel.Create(solarPanelConfig);
var battery = Battery.Create(batteryConfig);

// Add assets to the site
site.AddAsset(windTurbine);
site.AddAsset(solarPanel);
site.AddAsset(battery);

user.AddSite(site); // Add site to the user

var rxc = new RXCApp();
// Run the RXCApp
await rxc.Run(user, site);




