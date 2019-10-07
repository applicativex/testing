using Accounting.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Accounting.Host
{
    public class NbuCurrencyExchangeAdapter : ICurrencyExchangeAdapter
    {
        private const AccountCurrency BASE_CURRENCY = AccountCurrency.UAH;
        
        private readonly NbuCurrencyExchangeClient _nbuCurrencyExchangeClient;

        public NbuCurrencyExchangeAdapter(NbuCurrencyExchangeClient nbuCurrencyExchangeClient)
        {
            _nbuCurrencyExchangeClient = nbuCurrencyExchangeClient;
        }

        public async Task<decimal?> GetExchangeRate(AccountCurrency from, AccountCurrency to)
        {
            var rates = await _nbuCurrencyExchangeClient.GetExchangeRates();
            var fromBaseRate = from != BASE_CURRENCY ? rates.SingleOrDefault(x => x.Code == from.ToString())?.Rate : 1;
            var toBaseRate = to != BASE_CURRENCY ? rates.SingleOrDefault(x => x.Code == to.ToString())?.Rate : 1;
            return fromBaseRate / toBaseRate;
        }
    }
}
