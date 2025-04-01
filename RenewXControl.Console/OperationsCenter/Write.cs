using RenewXControl.Console.Domain.Assets;


namespace RenewXControl.Console.OperationsCenter
{
    /// <summary>
    /// Class responsible for writing data to various assets.
    /// </summary>
    public class Write
    {
       private readonly Asset _asset;
       /// <summary>
       /// Initializes a new instance of the <see cref="Write"/> class for a battery.
       /// </summary>
       /// <param name="battery">The battery asset.</param>
        public Write(Battery battery)
        {
            _asset = battery;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Write"/> class for a wind turbine.
        /// </summary>
        /// <param name="windTurbine">The wind turbine asset.</param>
        public Write(WindTurbine windTurbine)
        {
            _asset = windTurbine;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Write"/> class for a solar panel.
        /// </summary>
        /// <param name="solarPanel">The solar panel asset.</param>
        public Write (SolarPanel solarPanel)
        {
            _asset = solarPanel;
        }

        /// <summary>
        /// Sets the set point for the asset.
        /// </summary>
        /// <param name="value">The set point value.</param>
        public void SetSp(double value)
        {
            switch (_asset)
            {
                case Battery battery:
                    battery.SetSp(value);
                    break;
                case WindTurbine windTurbine:
                    windTurbine.SetSp(value);
                    break;
                case SolarPanel solarPanel:
                    solarPanel.SetSp(value);
                    break;
                default:
                    System.Console.WriteLine("Unsupported asset type");
                    break;
            }
        }
    }
}
