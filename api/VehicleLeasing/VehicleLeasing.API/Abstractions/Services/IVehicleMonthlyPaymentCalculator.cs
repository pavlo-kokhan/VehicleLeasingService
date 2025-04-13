namespace VehicleLeasing.API.Abstractions.Services;

public interface IVehicleMonthlyPaymentCalculator
{
    decimal Calculate(decimal vehicleEstimatedPrice, int advancePercentage, int months, decimal interestRate);
}