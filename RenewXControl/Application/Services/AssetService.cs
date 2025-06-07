using RenewXControl.Application.Interfaces;
using RenewXControl.Domain.Assets.Interfaces;

namespace RenewXControl.Application.Services
{
    public class AssetService : IAssetService
    {
        private readonly ISetPointAsset _setPointAsset;
        private readonly IGeneratorData _generatorData;
        public AssetService(ISetPointAsset setPointAsset, IGeneratorData generatorData)
        {
            _setPointAsset = setPointAsset;
            _generatorData = generatorData;
        }
        public void StartGenerator()
        {
            _generatorData.Start();
        }

        public void TurnOffGenerator()
        {
           _generatorData.Stop();
        }

        public void UpdateSetPoint(double amount)
        {
            _setPointAsset.UpdateSetPoint(amount);
        }

        public void UpdateSensor()
        {
            _generatorData.UpdateSensor();
        }

        public double GetSensor()
        {
            return _generatorData.GetSensor();
        }

        public void UpdateActivePower()
        {
            _generatorData.UpdateActivePower();
        }

        public double GetActivePower()
        {
            return _generatorData.GetActivePower();
        }
    }
}
