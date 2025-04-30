using System.Diagnostics;
using RenewXControl.Console.Domain.Assets;
using RenewXControl.Console.Domain.Users;

namespace RenewXControl.Console
{
    public class RXCApp
    {
        public async Task Run(User user, Site site)
        {
            Battery battery = null;
            SolarPanel solarPanel = null;
            WindTurbine windTurbine = null;

            foreach (var asset in site.Assets)
            {
                switch (asset)
                {
                    case Battery b:
                        battery = b;
                        break;
                    case SolarPanel sp:
                        solarPanel = sp;
                        break;
                    case WindTurbine wt:
                        windTurbine = wt;
                        break;

                    default:
                        System.Console.WriteLine($"Unknown asset type: {asset.GetType().Name}");
                        break;
                }
            }

            if (battery == null || solarPanel == null || windTurbine == null)
            {
                System.Console.WriteLine("One or more required assets are missing.");
            }

            // Print initial values once BEFORE entering the loop
            PrintInitial(battery, solarPanel, windTurbine);

            // Starting assets
            solarPanel.Start();
            windTurbine.Start();

            // Start the monitoring loop
            await StartMonitoring(battery, solarPanel, windTurbine);
        }

        private static void PrintInitial(
            Battery battery,
            SolarPanel solarPanel,
            WindTurbine windTurbine
        )
        {
            System.Console.Clear();
            System.Console.SetCursorPosition(0, 0);
            System.Console.ForegroundColor = ConsoleColor.Cyan;
            System.Console.WriteLine("====================================");
            System.Console.WriteLine("       ASSET MONITORING SYSTEM      ");
            System.Console.WriteLine("====================================\n");

            // Print Initial Wind Turbine Status
            System.Console.ForegroundColor = ConsoleColor.Blue;
            System.Console.WriteLine("=== Initial Wind Turbine Status ===");
            System.Console.ResetColor();
            System.Console.WriteLine($"Status:        {windTurbine.PowerStatusMessage}");
            System.Console.WriteLine($"Name:          {windTurbine.Name}");
            System.Console.WriteLine($"SetPoint:      {windTurbine.SetPoint} kW");
            System.Console.WriteLine($"Wind Speed:    {windTurbine.WindSpeed} km/h");
            System.Console.WriteLine($"Active Power:  {windTurbine.ActivePower} kW\n");

            // Print Initial Solar Panel Status
            System.Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.WriteLine("=== Initial Solar Panel Status ===");
            System.Console.ResetColor();
            System.Console.WriteLine($"Status:        {solarPanel.PowerStatusMessage}");
            System.Console.WriteLine($"Name:          {solarPanel.Name}");
            System.Console.WriteLine($"SetPoint:      {solarPanel.SetPoint} kW");
            System.Console.WriteLine($"Irradiance:    {solarPanel.Irradiance} W/m²");
            System.Console.WriteLine($"Active Power:  {solarPanel.ActivePower} kW\n");

            // Print Initial Battery Status
            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine("=== Initial Battery Status ===");
            System.Console.ResetColor();
            System.Console.WriteLine($"Name:          {battery.Name}");
            System.Console.WriteLine($"Capacity:      {battery.Capacity} kWh");
            System.Console.WriteLine($"State of Charge: {battery.StateOfCharge} %");
            System.Console.WriteLine($"SetPoint:      {battery.SetPoint} kW");
            System.Console.WriteLine($"Discharge Rate: {battery.FrequentlyOfDisCharge} kW\n");
            // Wait for user to acknowledge the initial status before starting the loop
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("\nPress any key to start live monitoring...");
            System.Console.ReadKey();
        }

        private async Task StartMonitoring(
            Battery battery,
            SolarPanel solarPanel,
            WindTurbine windTurbine
        )
        {
            // Please consider a small delay like Task.Delay(10) to prevent form putting too much prussure on CPU
            while (true)
            {
                // It's nice to keep your logging and calculating processes in 2 different methods and orchestrate them here in MonitoringStart method.
                System.Console.Clear(); // Clear previous output
                System.Console.SetCursorPosition(0, 0);

                System.Console.ForegroundColor = ConsoleColor.Cyan;
                System.Console.WriteLine("====================================");
                System.Console.WriteLine("       ASSET MONITORING SYSTEM      ");
                System.Console.WriteLine("====================================\n");

                // Wind Turbine Status
                System.Console.ForegroundColor = ConsoleColor.Blue;
                System.Console.WriteLine("=== Wind Turbine Status ===");
                System.Console.ResetColor();
                System.Console.Write("Status:        "); // Keep "Status:" in the default color
                System.Console.ForegroundColor =
                    windTurbine.SetPoint == 0 ? ConsoleColor.Red : ConsoleColor.Green;
                System.Console.WriteLine(windTurbine.PowerStatusMessage); // Change only the value color
                System.Console.ResetColor();
                System.Console.WriteLine($"Name:          {windTurbine.Name}");
                System.Console.WriteLine($"SetPoint:      {windTurbine.SetPoint} kW");
                System.Console.WriteLine($"Wind Speed:    {windTurbine.GetWindSpeed()} km/h");
                System.Console.WriteLine($"Active Power:  {windTurbine.GetAp()} kW\n");

                // Solar Panel Status
                System.Console.ForegroundColor = ConsoleColor.Yellow;
                System.Console.WriteLine("=== Solar Panel Status ===");
                System.Console.ResetColor();
                System.Console.Write("Status:        "); // Keep "Status:" in the default color
                System.Console.ForegroundColor =
                    solarPanel.SetPoint == 0 ? ConsoleColor.Red : ConsoleColor.Green;
                System.Console.WriteLine(solarPanel.PowerStatusMessage); // Change only the value color
                System.Console.ResetColor();
                System.Console.WriteLine($"Name:          {solarPanel.Name}");
                System.Console.WriteLine($"SetPoint:      {solarPanel.SetPoint} kW");
                System.Console.WriteLine($"Irradiance:    {solarPanel.GetIrradiance()} W/m²");
                System.Console.WriteLine($"Active Power:  {solarPanel.GetAp()} kW\n");

                // Battery Status
                System.Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine("=== Battery Status ===");
                System.Console.ResetColor();
                System.Console.Write("Status:        "); //
                System.Console.ForegroundColor = battery.IsNeedToCharge
                    ? ConsoleColor.Green
                    : ConsoleColor.Red;
                System.Console.WriteLine(battery.ChargeStateMessage);
                System.Console.ResetColor();
                System.Console.WriteLine($"Name:          {battery.Name}");
                System.Console.WriteLine($"Capacity:      {battery.Capacity} kWh");
                System.Console.WriteLine($"State of Charge: {battery.StateOfCharge} %");
                System.Console.WriteLine($"SetPoint:      {battery.SetPoint} kW");
                System.Console.WriteLine($"Discharge Rate: {battery.FrequentlyOfDisCharge} kW\n");

                // This is bussiness logic and should be encapsulated inside the battery entity
                // We would like to always keep logics (business rules, calculations, validations, etc) inside the entity and expose them via methods (behaviours)
                switch (battery)
                {
                    // charging
                    case { IsNeedToCharge: true, IsStartingCharge: false }:

                        solarPanel.SetSp();
                        solarPanel.PowerStatusMessage =
                            solarPanel.SetPoint != 0
                                ? "Solar is run.."
                                : "Solar is off.. we doesn't have good Irradiance";

                        windTurbine.SetSp();
                        windTurbine.PowerStatusMessage =
                            windTurbine.SetPoint != 0
                                ? "Turbine is run.."
                                : "Turbine is off.. we doesn't have good Wind speed";

                        _ = Task.Run(() => battery.Charge(solarPanel.GetAp(), windTurbine.GetAp()));
                        break;

                    // discharging
                    case { IsNeedToCharge: false, IsStartingCharge: false }:

                        solarPanel.Off();
                        solarPanel.PowerStatusMessage = "Solar is off..";

                        windTurbine.Off();
                        windTurbine.PowerStatusMessage = "Turbine is off..";

                        battery.SetSp();
                        _ = Task.Run(battery.Discharge);
                        break;
                }

                // Refresh every second
                await Task.Delay(1000);
            }
        }
    }
}
