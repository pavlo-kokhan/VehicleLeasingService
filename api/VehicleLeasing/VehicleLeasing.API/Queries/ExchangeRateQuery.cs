using MediatR;
using VehicleLeasing.API.Abstractions.Services;
using VehicleLeasing.API.Contracts.ExchangeRates;
using VehicleLeasing.API.Results.Generic;

namespace VehicleLeasing.API.Queries;

public class ExchangeRateQuery : IRequest<Result<List<ExchangeRateDto>>>
{
    public class Handler : IRequestHandler<ExchangeRateQuery, Result<List<ExchangeRateDto>>>
    {
        private readonly IExchangeRateService _exchangeRateService;

        public Handler(IExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
        }
        
        public async Task<Result<List<ExchangeRateDto>>> Handle(ExchangeRateQuery request, CancellationToken cancellationToken)
        {
            return await _exchangeRateService.GetRatesTableToUahAsync(DateOnly.FromDateTime(DateTime.Now));
        }
    }
}