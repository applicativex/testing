using Accounting.Domain;
using Accounting.Domain.Queries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Accounting.Service.Queries
{
    public class GetExchangeRatesQueryHandler : IRequestHandler<GetExchangeRatesQuery, GetExchangeRatesQueryResult>
    {
        private readonly ICurrencyExchangeAdapter _currencyExchangeAdapter;

        public GetExchangeRatesQueryHandler(ICurrencyExchangeAdapter currencyExchangeAdapter)
        {
            _currencyExchangeAdapter = currencyExchangeAdapter;
        }

        public async Task<GetExchangeRatesQueryResult> Handle(GetExchangeRatesQuery request, CancellationToken cancellationToken)
        {
            var eurRate = await _currencyExchangeAdapter.GetExchangeRate(AccountCurrency.UAH, AccountCurrency.EUR);
            var usdRate = await _currencyExchangeAdapter.GetExchangeRate(AccountCurrency.UAH, AccountCurrency.USD);
            return new GetExchangeRatesQueryResult
            {
                Rates = new CurrencyRateDto[]
                {
                    new CurrencyRateDto
                    {
                        Currency = AccountCurrency.EUR.ToString(),
                        Rate = eurRate.Value
                    },
                    new CurrencyRateDto
                    {
                        Currency = AccountCurrency.USD.ToString(),
                        Rate = usdRate.Value
                    }
                }
            };
        }
    }
}
