using Application.Interfaces.Asset;
using Domain.Entities.Assets;

namespace Application.DTOs;

public class MonitoringAssetControl
{
    public IAssetOperations AssetOperations { get; set; }
}