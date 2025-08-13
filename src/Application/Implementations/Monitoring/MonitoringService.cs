using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.User;
using Domain.Entities.Assets;

namespace Application.Implementations.Monitoring;

public class MonitoringService(
    IUsersService userService)
    : IMonitoringService
{
    public async Task<GeneralResponse<List<UserMonitoringInfo>>> GetAllUsersWithSitesAndAssets()
    {
        var listMonitoringInfo= await userService.GetAllUsersWithSitesAndAssetsAsync();
        if (listMonitoringInfo.Count == 0)
            return GeneralResponse<List<UserMonitoringInfo>>.Failure(message:"Does not have any information exist");

        return GeneralResponse<List<UserMonitoringInfo>>.Success(data:listMonitoringInfo);
    }
    public async Task ChargeDischarge(SolarPanel solar, WindTurbine turbine, Battery battery)
    {
        switch (battery.IsNeedToCharge)
        {
            // charging
            case true when battery.IsStartingChargeDischarge == false:
                solar.Start();
                turbine.Start();
                RecalculateTotalPower(solar,turbine,battery);
                await battery.Charge();
                break;

            // when battery need to update new total power for charging
            case true when battery.IsStartingChargeDischarge == true:
                RecalculateTotalPower(solar, turbine, battery);
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


    private static void RecalculateTotalPower(SolarPanel solar,WindTurbine turbine,Battery battery)
    {
        solar.UpdateActivePower();
        turbine.UpdateActivePower();

        var totalPower = solar.ActivePower + turbine.ActivePower;
        battery.SetTotalPower(totalPower);
    }
}