
namespace RenewXControl.Application.Interfaces.Assets
{
    public interface IAssetControl
    {
        void StartGenerators();
        Task ChargeDischarge();
        void TurnOffGenerators();

    }
}
