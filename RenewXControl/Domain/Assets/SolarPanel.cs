﻿using RenewXControl.Application.DTOs.AddAsset;

namespace RenewXControl.Domain.Assets
{
    public class SolarPanel : Asset
    {
        private SolarPanel() { }
        private SolarPanel(double irradiance, double activePower, double setPoint,Guid siteId)
        {
            Name = $"SP{Id}";
            Irradiance = irradiance;
            ActivePower = activePower;
            SetPoint = setPoint;
            SiteId = siteId;
        }

        public double Irradiance { get; private set; } // W/m²
        public double ActivePower { get; private set; } // kW
        public double SetPoint { get; private set; }  // Determines operation status

        public static SolarPanel Create(AddSolar addSolar, Guid siteId)
            => new SolarPanel(addSolar.Irradiance, addSolar.ActivePower, addSolar.SetPoint,siteId);

        public bool Start()
        {

            SetPoint = 10;
            if (Irradiance != 0.0)
            {
                return true;
            }
            else
            {
                ActivePower = 0;
                return false;
            }
        }
        public bool Stop()
        {
            ActivePower = SetPoint;
            return true;
        }

        public void UpdateSetPoint()
        {
            SetPoint = 0;
        }
        public bool UpdateIrradiance()
        {
            Irradiance = new Random().NextDouble() * 10;
            return Irradiance != 0.0 || SetPoint != 0.0;
        }
        public void UpdateActivePower()
        {
            ActivePower = Math.Min(Irradiance, SetPoint); // Assuming SetPoint is the maximum power output
        }
    }
}
