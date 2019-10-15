using System.Threading;
using System.Threading.Tasks;
using Accounting.Domain;
using Accounting.Domain.Commands.CurrencyExchange;
using MediatR;

namespace Accounting.Service.Commands
{
    public class BuyCurrencyCommandHandler : IRequestHandler<BuyCurrencyCommand, BuyCurrencyResult>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountTransactionRepository _accountTransactionRepository;
        private readonly ICurrencyExchangeAdapter _currencyExchangeAdapter;

        public BuyCurrencyCommandHandler(IAccountRepository accountRepository, IAccountTransactionRepository accountTransactionRepository, ICurrencyExchangeAdapter currencyExchangeAdapter)
        {
            _accountRepository = accountRepository;
            _accountTransactionRepository = accountTransactionRepository;
            _currencyExchangeAdapter = currencyExchangeAdapter;
        }

        public async Task<BuyCurrencyResult> Handle(BuyCurrencyCommand request, CancellationToken cancellationToken)
        {
            var baseAccount = await _accountRepository.FindAsync(request.AccountId);
            var currencyAccount = await _accountRepository.FindAsync(request.CurrencyAccountId);
            var exchangeBaseAccount = SystemAccount.FromCurrency(baseAccount.Currency);
            var exchangeCurrencyAccount = SystemAccount.FromCurrency(currencyAccount.Currency);

            var exchangeRate = await _currencyExchangeAdapter.GetExchangeRate(currencyAccount.Currency, baseAccount.Currency);
            var baseAmount = request.Amount * exchangeRate;

            var debitBaseAccount = baseAccount.Transfer(exchangeBaseAccount, baseAmount.Value, $"Buy {request.Amount} {currencyAccount.Currency.ToString()} operation");
            var creditCurrencyAccount = exchangeCurrencyAccount.Transfer(currencyAccount, request.Amount, $"Buy {request.Amount} {currencyAccount.Currency.ToString()} operation");

            await _accountTransactionRepository.SaveAsync(debitBaseAccount);
            await _accountTransactionRepository.SaveAsync(creditCurrencyAccount);
            return new BuyCurrencyResult();
        }
    }
}
