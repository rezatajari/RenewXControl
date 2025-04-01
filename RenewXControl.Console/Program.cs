using System.Security.AccessControl;
using System.Text.Json;
using RenewXControl.Console.Domain.Assets;
using RenewXControl.Console.InitConfiguration.AssetsModelConfig;
using System.Threading.Tasks;
using System.Xml.Serialization;

var jsonAssetsConfiguration = File.ReadAllText(path: @"D:\Repo\RenewXControl\RenewXControl.Console\InitConfiguration\JsonConfigurationSetting\Asset.json");
var assets = JsonSerializer.Deserialize<AssetsConfig>(jsonAssetsConfiguration);

var windTurbine = new WindTurbine(assets.WindTurbineConfig);
var solarPanel = new SolarPanel(assets.SolarPanelConfig);
var battery = new Battery(assets.BatteryConfig);

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

        // charging 
        if (battery is { IsNeedToCharge: true, IsStartingCharge: false })
        {
            solarPanel.SetSp(2.1);
            solarPanel.PowerStatusMessage = $"Solar is run..";
            windTurbine.SetSp(3.1);
            windTurbine.PowerStatusMessage = $"Turbine is run..";
            _ = Task.Run(() => battery.Charge(solarPanel.GetAp(), windTurbine.GetAp()));

        }
        // discharging 
        else if (battery is { IsNeedToCharge: false, IsStartingCharge: false })
        {
            solarPanel.SetSp(0); 
            solarPanel.PowerStatusMessage = "Solar is off..";
            windTurbine.SetSp(0); 
            windTurbine.PowerStatusMessage = "Turbine is off..";
            _ = Task.Run(() => battery.Discharge());
        }

        // Refresh every second
        await Task.Delay(1000);
    }
}
