using RenewXControl.Domain.Assets;

namespace RenewXControl.Application.Interfaces
{
    public interface IAssetControl
    {
        Task ChargeDischarge();
    }
}
