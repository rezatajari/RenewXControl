using System.Text.Json;
using RenewXControl.Console.Domain.Assets;
using RenewXControl.Console.Domain.Users;
using RenewXControl.Console.InitConfiguration.AssetsModelConfig.Assets;
using RenewXControl.Console.InitConfiguration.AssetsModelConfig.Users;


var jsonUserConfig =
    File.ReadAllText(
        path: @"D:\Repo\RenewXControl\RenewXControl.Console\InitConfiguration\JsonConfigurationSetting\User.json");
var userConfig = JsonSerializer.Deserialize<UserConfig>(jsonUserConfig);

var jsonSiteConfig =
    File.ReadAllText(
        path: @"D:\Repo\RenewXControl\RenewXControl.Console\InitConfiguration\JsonConfigurationSetting\Site.json");
var siteConfig = JsonSerializer.Deserialize<SiteConfig>(jsonSiteConfig);

var jsonSolarConfig =
    File.ReadAllText(
        path: @"D:\Repo\RenewXControl\RenewXControl.Console\InitConfiguration\JsonConfigurationSetting\SolarPanel.json");
var solarConfig = JsonSerializer.Deserialize<SolarPanelConfig>(jsonSolarConfig);

var jsonTurbineConfig =
    File.ReadAllText(
        path: @"D:\Repo\RenewXControl\RenewXControl.Console\InitConfiguration\JsonConfigurationSetting\WindTurbine.json");
var windTurbineConfig = JsonSerializer.Deserialize<WindTurbineConfig>(jsonTurbineConfig);

var jsonBatteryConfig =
    File.ReadAllText(
        path: @"D:\Repo\RenewXControl\RenewXControl.Console\InitConfiguration\JsonConfigurationSetting\Battery.json");
var batteryConfig = JsonSerializer.Deserialize<BatteryConfig>(jsonBatteryConfig);


var user = new User(userConfig);
var site = new Site(siteConfig, user.Id);
var windTurbine = new WindTurbine(windTurbineConfig, site.Id);
var solarPanel = new SolarPanel(solarConfig,site.Id);
var battery = new Battery(batteryConfig,site.Id);
site.AddAsset(windTurbine);
site.AddAsset(solarPanel);
site.AddAsset(battery);
user.AddSite(site);


// Print initial values once BEFORE entering the loop
PrintInitial();
// Starting assets
StartAssets();
// Now Start the Monitoring Loop for Continuous Updates
await Task.Run(MonitoringShowInformation);

void PrintInitial()
{
    Console.Clear();
    Console.SetCursorPosition(0, 0);
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("====================================");
    Console.WriteLine("       ASSET MONITORING SYSTEM      ");
    Console.WriteLine("====================================\n");

    // Print Initial Wind Turbine Status
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("=== Initial Wind Turbine Status ===");
    Console.ResetColor();
    Console.WriteLine($"Status:        {windTurbine.PowerStatusMessage}");
    Console.WriteLine($"Name:          {windTurbine.Name}");
    Console.WriteLine($"SetPoint:      {windTurbine.SetPoint} kW");
    Console.WriteLine($"Wind Speed:    {windTurbine.WindSpeed} km/h");
    Console.WriteLine($"Active Power:  {windTurbine.ActivePower} kW\n");

    // Print Initial Solar Panel Status
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("=== Initial Solar Panel Status ===");
    Console.ResetColor();
    Console.WriteLine($"Status:        {solarPanel.PowerStatusMessage}");
    Console.WriteLine($"Name:          {solarPanel.Name}");
    Console.WriteLine($"SetPoint:      {solarPanel.SetPoint} kW");
    Console.WriteLine($"Irradiance:    {solarPanel.Irradiance} W/m²");
    Console.WriteLine($"Active Power:  {solarPanel.ActivePower} kW\n");

    // Print Initial Battery Status
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("=== Initial Battery Status ===");
    Console.ResetColor();
    Console.WriteLine($"Name:          {battery.Name}");
    Console.WriteLine($"Capacity:      {battery.Capacity} kWh");
    Console.WriteLine($"State of Charge: {battery.StateOfCharge} %");
    Console.WriteLine($"SetPoint:      {battery.SetPoint} kW");
    Console.WriteLine($"Discharge Rate: {battery.FrequentlyOfDisCharge} kW\n");

    // Wait for user to acknowledge the initial status before starting the loop
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("\nPress any key to start live monitoring...");
    Console.ReadKey();

}
void StartAssets()
{
    solarPanel.Start();
    windTurbine.Start();
}
async Task MonitoringShowInformation()
{
    while (true)
    {
        Console.Clear(); // Clear previous output
        Console.SetCursorPosition(0, 0);

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("====================================");
        Console.WriteLine("       ASSET MONITORING SYSTEM      ");
        Console.WriteLine("====================================\n");

        // Wind Turbine Status
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("=== Wind Turbine Status ===");
        Console.ResetColor();
        Console.Write("Status:        "); // Keep "Status:" in the default color
        Console.ForegroundColor = windTurbine.SetPoint == 0 ? ConsoleColor.Red : ConsoleColor.Green;
        Console.WriteLine(windTurbine.PowerStatusMessage); // Change only the value color
        Console.ResetColor();
        Console.WriteLine($"Name:          {windTurbine.Name}");
        Console.WriteLine($"SetPoint:      {windTurbine.SetPoint} kW");
        Console.WriteLine($"Wind Speed:    {windTurbine.GetWindSpeed()} km/h");
        Console.WriteLine($"Active Power:  {windTurbine.GetAp()} kW\n");

        // Solar Panel Status
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("=== Solar Panel Status ===");
        Console.ResetColor();
        Console.Write("Status:        "); // Keep "Status:" in the default color
        Console.ForegroundColor = solarPanel.SetPoint == 0 ? ConsoleColor.Red : ConsoleColor.Green;
        Console.WriteLine(solarPanel.PowerStatusMessage); // Change only the value color
        Console.ResetColor();
        Console.WriteLine($"Name:          {solarPanel.Name}");
        Console.WriteLine($"SetPoint:      {solarPanel.SetPoint} kW");
        Console.WriteLine($"Irradiance:    {solarPanel.GetIrradiance()} W/m²");
        Console.WriteLine($"Active Power:  {solarPanel.GetAp()} kW\n");

        // Battery Status
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("=== Battery Status ===");
        Console.ResetColor();
        Console.Write("Status:        "); //
        Console.ForegroundColor = battery.IsNeedToCharge ? ConsoleColor.Green : ConsoleColor.Red;
        Console.WriteLine(battery.ChargeStateMessage);
        Console.ResetColor();
        Console.WriteLine($"Name:          {battery.Name}");
        Console.WriteLine($"Capacity:      {battery.Capacity} kWh");
        Console.WriteLine($"State of Charge: {battery.StateOfCharge} %");
        Console.WriteLine($"SetPoint:      {battery.SetPoint} kW");
        Console.WriteLine($"Discharge Rate: {battery.FrequentlyOfDisCharge} kW\n");

        switch (battery)
        {
            // charging 
            case { IsNeedToCharge: true, IsStartingCharge: false }:

                solarPanel.SetSp();
                solarPanel.PowerStatusMessage = solarPanel.SetPoint != 0 ? "Solar is run.." : "Solar is off.. we doesn't have good Irradiance";

                windTurbine.SetSp();
                windTurbine.PowerStatusMessage = windTurbine.SetPoint != 0 ? "Turbine is run.." : "Turbine is off.. we doesn't have good Wind speed";

                _ = Task.Run(() => battery.Charge(solarPanel.GetAp(), windTurbine.GetAp()));
                break;

            // discharging 
            case { IsNeedToCharge: false, IsStartingCharge: false }:

                solarPanel.Off();
                solarPanel.PowerStatusMessage = "Solar is off..";

                windTurbine.Off();
                windTurbine.PowerStatusMessage = "Turbine is off..";

                battery.SetSp();
                _ = Task.Run(() => battery.Discharge());
                break;
        }

        // Refresh every second
        await Task.Delay(1000);
    }
}

