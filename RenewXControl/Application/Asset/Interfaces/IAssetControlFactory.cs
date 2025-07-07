using RenewXControl.Domain.Assets;
using RenewXControl.Domain.Interfaces.Assets;

namespace RenewXControl.Application.Asset.Interfaces
{
    public interface IAssetControlFactory
    {
        ISolarControl CreateSolarControlAsync(SolarPanel solar);
        ITurbineControl CreateTurbineControlAsync(WindTurbine turbine);
        IBatteryControl CreateBatteryControlAsync(Battery battery);
        IAssetRuntimeOperation CreateAssetRuntimeOperationAsync(ISolarControl solarControl,ITurbineControl turbineControl,IBatteryControl batteryControl);
    }
}
