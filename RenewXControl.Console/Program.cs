using RenewXControl.Console;
using RenewXControl.Console.Configuration;
using RenewXControl.Console.Domain.Assets;
using RenewXControl.Console.Domain.Users;
using RenewXControl.Console.InitConfiguration.AssetsModelConfig.Assets;
using RenewXControl.Console.InitConfiguration.AssetsModelConfig.Users;

// Configuration json files
var userConfig = ConfigurationSetting.ReadConfig<UserConfig>(fileName: "User.json");
var siteConfig = ConfigurationSetting.ReadConfig<SiteConfig>(fileName: "Site.json");
var solarPanelConfig = ConfigurationSetting.ReadConfig<SolarPanelConfig>(fileName: "SolarPanel.json");
var windTurbineConfig = ConfigurationSetting.ReadConfig<WindTurbineConfig>(fileName: "WindTurbine.json");
var batteryConfig = ConfigurationSetting.ReadConfig<BatteryConfig>(fileName: "Battery.json");

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




