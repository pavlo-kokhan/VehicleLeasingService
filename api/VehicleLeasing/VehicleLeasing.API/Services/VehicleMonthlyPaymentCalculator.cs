using VehicleLeasing.API.Abstractions.Services;

namespace VehicleLeasing.API.Services;

public class VehicleMonthlyPaymentCalculator : IVehicleMonthlyPaymentCalculator
{
    public decimal Calculate(decimal vehicleEstimatedPrice, int advancePercentage, int months, decimal interestRate)
    {
        var leasingBody = vehicleEstimatedPrice * (1 - advancePercentage / 100m);

        var totalWithInterest = leasingBody * (1 + interestRate);

        return Math.Round(totalWithInterest / months, 2);
    }
}