namespace Domain.Interfaces.Assets;

public interface ISolarControl:ICommonEnergyControl
{
    bool UpdateIrradiance();
    double Irradiance { get; }
}