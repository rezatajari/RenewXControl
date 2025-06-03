using Microsoft.Extensions.Configuration;
using RenewXControl;
using RenewXControl.Configuration.AssetsModel.Assets;
using RenewXControl.Configuration.AssetsModel.Users;
using RenewXControl.Domain.Assets;
using RenewXControl.Domain.Users;




var con = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory()) 
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) 
    .Build(); 

// Bind and register configuration
var batteryConfig = con.GetSection("BatteryConfig").Get<BatteryConfig>();
var solarPanelConfig = con.GetSection("SolarPanelConfig").Get<SolarPanelConfig>();
var windTurbineConfig = con.GetSection("WindTurbineConfig").Get<WindTurbineConfig>();
var userConfig = con.GetSection("UserConfig").Get<UserConfig>();
var siteConfig = con.GetSection("SiteConfig").Get<SiteConfig>();


// Map binding to our entity
var user = User.Create(userConfig.Name);
var site = Site.Create(siteConfig);
var windTurbine = new WindTurbine(windTurbineConfig);
var solarPanel = SolarPanel.Create(solarPanelConfig)
var battery = Battery.Create(batteryConfig);

// Add assets to the site
site.AddAsset(windTurbine);
site.AddAsset(solarPanel);
site.AddAsset(battery);

user.AddSite(site); // Add site to the user

var app = new RXCApp();
// Run the RXCApp
await app.Run(user,site);




