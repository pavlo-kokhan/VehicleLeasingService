namespace VehicleLeasing.API.Commands.Vehicles.Abstract;

public interface IVehicleCommand
{
    string Brand { get; }
    string Model { get; }
    int Year { get; }
    decimal EstimatedPrice { get; }
}