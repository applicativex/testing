using System.Threading.Tasks;

namespace Accounting.Domain
{
    public interface ICurrencyExchangeAdapter
    {
        Task<decimal?> GetExchangeRate(AccountCurrency from, AccountCurrency to);
    }
}
