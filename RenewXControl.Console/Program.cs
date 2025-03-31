using System.Text.Json;
using RenewXControl.Console.Domain.Assets;
using RenewXControl.Console.InitConfiguration.AssetsModelConfig;

var jsonAssetsConfiguration=File.ReadAllText(path:@"D:\Repo\RenewXControl\RenewXControl.Console\InitConfiguration\JsonConfigurationSetting\Asset.json");
var assets = JsonSerializer.Deserialize<AssetsConfig>(jsonAssetsConfiguration);

var windTurbine = new WindTurbine(assets.WindTurbineConfig);
var solarPanel = new SolarPanel(assets.SolarPanelConfig);
var battery = new Battery(assets.BatteryConfig);

//while (true)
//{
    Console.Clear(); // Clear previous output
    Console.SetCursorPosition(0, 0); // Move cursor to top-left

    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("====================================");
    Console.WriteLine("       ASSET MONITORING SYSTEM      ");
    Console.WriteLine("====================================\n");

    // Wind Turbine Status
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("=== Wind Turbine Status ===");
    Console.ResetColor();
    Console.WriteLine($"SetPoint:      {windTurbine.SetPoint} kW");
    Console.WriteLine($"Wind Speed:    {windTurbine.WindSpeed} km/h");
    Console.WriteLine($"Active Power:  {windTurbine.ActivePower} kW\n");

    // Solar Panel Status
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("=== Solar Panel Status ===");
    Console.ResetColor();
    Console.WriteLine($"SetPoint:      {solarPanel.SetPoint} kW");
    Console.WriteLine($"Irradiance:    {solarPanel.Irradiance} W/m²");
    Console.WriteLine($"Active Power:  {solarPanel.ActivePower} kW\n");

    // Battery Status
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("=== Battery Status ===");
    Console.ResetColor();
    Console.WriteLine($"Capacity:      {battery.Capacity} kWh");
    Console.WriteLine($"State of Charge: {battery.StateOfCharge}%");
    Console.WriteLine($"SetPoint:      {battery.SetPoint} kW");
    Console.WriteLine($"Discharge Rate: {battery.FrequentlyOfDisCharge} kW\n");

    // Refresh every second
    Thread.Sleep(1000);

    Console.ReadLine();
//}

//static double GetRandomValue()
//{
//    var random = new Random();
//    return Math.Round(random.NextDouble() * 10, 2); // Generates a random value between 0 and 10
//}