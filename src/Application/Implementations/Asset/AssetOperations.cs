using Application.Interfaces.Asset;
using Domain.Entities.Assets;

namespace Application.Implementations.Asset
{
    public class AssetOperations(
        SolarPanel solar,
        WindTurbine turbine,
        Battery battery)
        : IAssetOperations
    {
        public async Task ChargeDischarge()
        {
            switch (battery.IsNeedToCharge)
            {
                // charging
                case true when battery.IsStartingChargeDischarge == false:
                    solar.Start();
                    turbine.Start();
                    RecalculateTotalPower();
                    await battery.Charge();
                    break;

                // when battery need to update new total power for charging
                case true when battery.IsStartingChargeDischarge == true:
                    RecalculateTotalPower();
                    break;

                // discharging
                case false when battery.IsStartingChargeDischarge == false:
                    // UpdateSetPointGenerators
                    solar.UpdateSetPoint();
                    turbine.UpdateSetPoint();

                    solar.Stop();
                    turbine.Stop();
                    await battery.Discharge();
                    break;
            }
        }

        private void RecalculateTotalPower()
        {
            solar.UpdateActivePower();
            turbine.UpdateActivePower();

            var totalPower = solar.ActivePower + turbine.ActivePower;
            battery.SetTotalPower(totalPower);
        }
    }
}
