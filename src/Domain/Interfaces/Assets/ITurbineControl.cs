namespace Domain.Interfaces.Assets;

public interface ITurbineControl:ICommonEnergyControl
{
    bool UpdateWindSpeed();
    double WindSpeed { get; }
}