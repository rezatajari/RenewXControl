using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RenewXControl.Console;
using Microsoft.Extensions.Options;
using RenewXControl.Console.Domain.Assets;
using RenewXControl.Console.Domain.Users;




var con = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory()) // Set the base path
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // Load appsettings.json
    .Build();  // Build the configuration

// Bind and register configuration
var batteryConfig = con.GetSection("BatteryConfig").Get<BatteryConfig>();
var solarPanelConfig = con.GetSection("BatteryConfig").Get<SolarPanelConfig>();
var windTurbineConfig = con.GetSection("BatteryConfig").Get<WindTurbineConfig>();
var userConfig = con.GetSection("BatteryConfig").Get<UserConfig>();
var siteConfig = con.GetSection("BatteryConfig").Get<SiteConfig>();


// Map binding to our entity
var user = new User(userConfig);
var site = new Site(siteConfig, user.Id);
var windTurbine = new WindTurbine(windTurbineConfig);
var solarPanel = new SolarPanel(solarPanelConfig);
var battery = new Battery(batteryConfig);

// Add assets to the site
site.AddAsset(windTurbine);
site.AddAsset(solarPanel);
site.AddAsset(battery);

user.AddSite(site); // Add site to the user

var app = new RXCApp();
// Run the RXCApp
await app.Run(user,site,windTurbine,solarPanel,battery);




