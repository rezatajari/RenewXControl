using RenewXControl.Domain.Assets;

namespace RenewXControl.Application.Interfaces.Assets
{
    public interface IAssetControl
    {
        Task ChargeDischarge();
    }
}
