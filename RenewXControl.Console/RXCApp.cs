using RenewXControl.Console.Domain.Assets;
using RenewXControl.Console.Domain.Users;


namespace RenewXControl.Console
{
    public class RXCApp
    {
        public async Task Run(User user, Site site, WindTurbine windTurbine, SolarPanel solarPanel, Battery battery)
        {
            // Print initial values once BEFORE entering the loop
            PrintInitial(windTurbine, solarPanel, battery);

            // Starting assets
            solarPanel.Start();
            windTurbine.Start();

            // Start the monitoring loop
            await MonitoringStart(windTurbine, solarPanel, battery);

        }

        private static void PrintInitial(WindTurbine windTurbine, SolarPanel solarPanel, Battery battery)
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

        private async Task MonitoringStart(WindTurbine windTurbine, SolarPanel solarPanel, Battery battery)
        {
            while (true)
            {
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
                System.Console.ForegroundColor = windTurbine.SetPoint == 0 ? ConsoleColor.Red : ConsoleColor.Green;
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
                System.Console.ForegroundColor = solarPanel.SetPoint == 0 ? ConsoleColor.Red : ConsoleColor.Green;
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
                System.Console.ForegroundColor = battery.IsNeedToCharge ? ConsoleColor.Green : ConsoleColor.Red;
                System.Console.WriteLine(battery.ChargeStateMessage);
                System.Console.ResetColor();
                System.Console.WriteLine($"Name:          {battery.Name}");
                System.Console.WriteLine($"Capacity:      {battery.Capacity} kWh");
                System.Console.WriteLine($"State of Charge: {battery.StateOfCharge} %");
                System.Console.WriteLine($"SetPoint:      {battery.SetPoint} kW");
                System.Console.WriteLine($"Discharge Rate: {battery.FrequentlyOfDisCharge} kW\n");

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
                        _ = Task.Run(battery.Discharge);
                        break;
                }

                // Refresh every second
                await Task.Delay(1000);
            }
        }
    }
}
