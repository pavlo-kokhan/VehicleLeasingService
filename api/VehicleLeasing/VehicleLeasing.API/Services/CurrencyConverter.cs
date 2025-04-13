using VehicleLeasing.API.Constants;
using VehicleLeasing.DataAccess.DbContexts;

namespace VehicleLeasing.API.Services;

public class CurrencyConverter
{
    private readonly VehicleLeasingDbContext _context;

    public CurrencyConverter(VehicleLeasingDbContext context)
    {
        _context = context;
    }

    public async Task<decimal> ConvertFromUsd(decimal amountUsd, string targetCurrencyCode, 
        DateOnly dateOnly, CancellationToken cancellationToken = default)
    {
        if (targetCurrencyCode.Equals(CurrencyCodes.Usd, StringComparison.OrdinalIgnoreCase))
            return amountUsd;
    
        // var rate = await _context.ExchangeRates
        //     .Where(r => r.CurrencyCode == targetCurrencyCode.ToUpper() && r.ExchangeDate == date.Date)
        //     .Select(r => r.Rate)
        //     .FirstOrDefaultAsync(cancellationToken);
        
        return 0;
    }
}