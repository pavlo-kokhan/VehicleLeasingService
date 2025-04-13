using MediatR;
using Microsoft.EntityFrameworkCore;
using VehicleLeasing.API.Abstractions.Services;
using VehicleLeasing.API.Constants;
using VehicleLeasing.API.Constants.Errors;
using VehicleLeasing.API.Contracts.Common;
using VehicleLeasing.API.Contracts.Vehicles;
using VehicleLeasing.API.Results.Generic;
using VehicleLeasing.DataAccess.DbContexts;

namespace VehicleLeasing.API.Queries.Vehicles;

public record VehicleLeasingCalculatorQuery(
    string Category,
    string Brand,
    string Model,
    int LeasingMonths,
    int AdvancePercentage) : IRequest<Result<VehicleForLeasingResponse>>
{
    public class Handler : IRequestHandler<VehicleLeasingCalculatorQuery, Result<VehicleForLeasingResponse>>
    {
        private readonly VehicleLeasingDbContext _context;
        private readonly IExchangeRateService _exchangeRateService;
        private readonly IVehicleMonthlyPaymentCalculator _monthlyPaymentCalculator;

        public Handler(VehicleLeasingDbContext context, IExchangeRateService exchangeRateService, IVehicleMonthlyPaymentCalculator monthlyPaymentCalculator)
        {
            _context = context;
            _exchangeRateService = exchangeRateService;
            _monthlyPaymentCalculator = monthlyPaymentCalculator;
        }

        public async Task<Result<VehicleForLeasingResponse>> Handle(VehicleLeasingCalculatorQuery request, CancellationToken cancellationToken)
        {
            var vehicleEntity = await _context.Vehicles
                .Include(v => v.Status)
                .Include(v => v.Category)
                .Include(v => v.Transmission)
                .Include(v => v.FuelType)
                .Where(v => v.Status.Status == VehicleStatuses.Available
                            && v.Brand == request.Brand
                            && v.Model == request.Model
                            && v.Category.Category.ToLower().Replace(" ", string.Empty) == request.Category.ToLower().Replace(" ", string.Empty)
                            && v.Status.Status == VehicleStatuses.Available)
                .FirstOrDefaultAsync(cancellationToken);
                
            if (vehicleEntity is null) 
                return VehiclesValidationErrors.VehicleNotFound;
            
            var interestRate = await _context.LeasingInterestRates
                .Where(r => request.LeasingMonths >= r.MinMonths 
                            && request.LeasingMonths <= r.MaxMonths
                            && request.AdvancePercentage >= r.MinAdvancePercent
                            && request.AdvancePercentage <= r.MaxAdvancePercent)
                .Select(r => r.InterestRate)
                .FirstOrDefaultAsync(cancellationToken);

            if (interestRate == default)
                return LeasingInterestRateValidationErrors.InterestRateNotFound;

            var monthlyPayment = _monthlyPaymentCalculator.Calculate(
                vehicleEntity.EstimatedPrice,
                request.AdvancePercentage, 
                request.LeasingMonths, 
                interestRate);

            var exchangeRates = await _exchangeRateService.GetRatesAsync(
                DateOnly.FromDateTime(DateTime.Today));
            
            var rateEur = exchangeRates.FirstOrDefault(r => r.CurrencyCode == CurrencyCodes.Eur)
                ?.Rate;
            
            var rateUah = exchangeRates.FirstOrDefault(r => r.CurrencyCode == CurrencyCodes.Uah)
                ?.Rate;

            return new VehicleForLeasingResponse(
                vehicleEntity.Id,
                vehicleEntity.Brand,
                vehicleEntity.Model,
                vehicleEntity.Year,
                vehicleEntity.EstimatedPrice,
                new MultiCurrencyPriceResponse(
                    monthlyPayment,
                    rateEur.HasValue ? monthlyPayment * rateEur.Value : 0m,
                    rateUah.HasValue ? monthlyPayment * rateUah.Value : 0m),
                vehicleEntity.Category.Category,
                vehicleEntity.Transmission.Transmission,
                vehicleEntity.FuelType.Type,
                vehicleEntity.Status.Status);
        }
    }
}