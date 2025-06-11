namespace RenewXControl.Application.Interfaces.Assets
{
    public interface IAssetService
    {
        void StartGenerators();
        Task ChargeDischarge();
        void TurnOffGenerators();

    }
}
